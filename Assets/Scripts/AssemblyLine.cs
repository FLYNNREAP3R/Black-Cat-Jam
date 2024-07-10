using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class AssemblyLine : MonoBehaviour
{
    private bool isDisabled = false;

    [SerializeField] private GameObject Test;
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private GameObject ItemContainer;
    [SerializeField] private Transform[] PathNodes;

    [SerializeField] private float minInteractionDistance = 0.5f;
    [SerializeField] private Transform interactionZone;

    [SerializeField] private DeliveryTruck deliveryTruck;

    [SerializeField] private TextMeshProUGUI packageKeyText;
    [SerializeField] private TextMeshProUGUI yeetKeyText;

    [SerializeField] public int assemblyLineNumber;
    private string packageActionName = "AssemblyLine_P";
    private string yeetActionName = "AssemblyLine_S";

    [SerializeField] private List<Sprite> boxSprites;

    [SerializeField] private PlayerInput playerInput;

    [Header("Animation And Sprites")]
    [SerializeField] private Animator beltAnimator;
    [SerializeField] private Animator catAnimator;

    [SerializeField] private GameObject OosSign;
    [SerializeField] private GameObject CatPackager;
    [SerializeField] private GameObject ButtonSign;

    [SerializeField] private List<AudioClip> HitAudioClips;
    [SerializeField] private List<AudioClip> YeetAudioClips;
    private List<AudioSource> AudioSources;

    // Start is called before the first frame update
    void Start()
    {
        packageActionName += assemblyLineNumber;
        yeetActionName += assemblyLineNumber;

        packageKeyText.text = playerInput.actions[packageActionName].bindings[0].ToDisplayString();
        yeetKeyText.text = playerInput.actions[yeetActionName].bindings[0].ToDisplayString();

        AudioSources = new List<AudioSource>(GetComponents<AudioSource>());

    }

    // Update is called once per frame
    void Update()
    {
        if (isDisabled)
            return;

        //if (Input.GetKeyDown(packageKeyCode))
        if (playerInput.actions[packageActionName].triggered)
        {
            PackageItem();
        }

        if (playerInput.actions[yeetActionName].triggered)
        {
            YeetItem();
        }
    }

    private void PackageItem()
    {
        GameObject itemToPackage = GetClosestItem()?.gameObject;
        catAnimator.SetTrigger("Package");

        var hitAudioSource = AudioSources.First(x => x.clip == null || (x.clip.name != "yeah" && x.clip.name != "nah"));
        hitAudioSource.volume = GameSettings.Instance.volume / 100f * 0.9375f;
        hitAudioSource.clip = HitAudioClips[Random.Range(0, HitAudioClips.Count)];
        hitAudioSource.Play();

        if (itemToPackage != null)
        {
            int itemScore = itemToPackage.GetComponent<ItemStats>().itemScore;

            // checks for good or bad item
            if (itemToPackage.tag.Equals("Package"))
            {
                AudioSources.First(x => x.clip.name == "nah").Play();
                GameManager.Instance.UpdateScore(-1);
            }
            else
            {

                if(itemToPackage.tag.Equals("Bad"))
                    AudioSources.First(x => x.clip.name == "nah").Play();
                else
                    AudioSources.First(x => x.clip.name == "yeah").Play();

                //Load Truck
                deliveryTruck.LoadBox();

                //Disable item animator
                Animator itemAnimator = itemToPackage.GetComponent<Animator>();
                if (itemAnimator != null)
                    itemAnimator.enabled = false;

                Sprite randomBoxSprite = boxSprites[Random.Range(0, boxSprites.Count)];
                SpriteRenderer renderer = itemToPackage.GetComponent<SpriteRenderer>();
                renderer.sprite = randomBoxSprite;
                itemToPackage.tag = "Package";

                

                GameManager.Instance.UpdateScore(itemScore);
                
            }
        }
        else
        {
            AudioSources.First(x => x.clip.name == "nah").Play();
            GameManager.Instance.UpdateScore(-1);
        }
    }

    private void YeetItem()
    {
        GameObject itemToYeet = GetClosestItem()?.gameObject;
        catAnimator.SetTrigger("Yeet");

        var yeetAudioSource = AudioSources.First(x => x.clip == null || x.clip.name != "yeah" && x.clip.name != "nah");
        yeetAudioSource.volume = GameSettings.Instance.volume / 100f * 0.9375f;
        yeetAudioSource.clip = YeetAudioClips[Random.Range(0, YeetAudioClips.Count)];
        yeetAudioSource.Play();

        if (itemToYeet != null)
        {
            int itemScore = itemToYeet.GetComponent<ItemStats>().itemScore;

            if (itemToYeet.tag.Equals("Bad"))
            {
                AudioSources.First(x => x.clip.name == "yeah").Play();
            }
            else
            { 
                AudioSources.First(x => x.clip.name == "nah").Play();
            }

            GameManager.Instance.UpdateScore(itemScore * -1);

            Destroy(itemToYeet);
        }
        else
        {
            AudioSources.First(x => x.clip.name == "nah").Play();
            GameManager.Instance.UpdateScore(-1);
        }
    }

    //Spawn An Item
    public void SpawnItem(GameObject gameObject)
    {
        GameObject item = Instantiate(gameObject, SpawnLocation.position, Quaternion.identity);
        item.transform.parent = ItemContainer.transform;
        item.GetComponent<ItemMovement>().path = PathNodes;

        if (assemblyLineNumber == 2 || assemblyLineNumber == 4)
            item.GetComponent<SpriteRenderer>().flipX = true;
    }

    private Transform GetClosestItem()
    {
        Transform closestItem = null;
        float closestDistance = 100000f;

        // create empty transform array
        Transform[] allItems = new Transform[ItemContainer.transform.childCount];

        // populate array with children
        for (int i = 0; i < allItems.Length; i++)
        {
            allItems[i] = ItemContainer.transform.GetChild(i);
        }

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        var scaleFactor = width / height / (16f / 9.05f);

        // check for closest distance
        foreach (Transform item in allItems)
        {
            float dist = Vector3.Distance(item.position, interactionZone.position);
            if (dist < closestDistance && dist < minInteractionDistance * System.Math.Min(1, scaleFactor))
            {
                closestItem = item;
                closestDistance = dist;
            }
        }

        return closestItem;
    }

    public void Disable() 
    {
        ButtonSign.gameObject.SetActive(false);
        CatPackager.gameObject.SetActive(false);
        OosSign.gameObject.SetActive(true);
        beltAnimator.enabled = false;
        isDisabled = true;
    }

    // Draw on debug to show interaction distance on assembly line
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(interactionZone.position, minInteractionDistance);
    //}
}
