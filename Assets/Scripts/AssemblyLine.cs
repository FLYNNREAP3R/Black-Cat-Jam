using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

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

    // Start is called before the first frame update
    void Start()
    {
        packageActionName += assemblyLineNumber;
        yeetActionName += assemblyLineNumber;

        packageKeyText.text = playerInput.actions[packageActionName].bindings[0].ToDisplayString();
        yeetKeyText.text = playerInput.actions[yeetActionName].bindings[0].ToDisplayString();
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

        if (itemToPackage != null)
        {
            int itemScore = itemToPackage.GetComponent<ItemStats>().itemScore;

            // checks for good or bad item
            if (itemToPackage.tag.Equals("Package"))
            {
                GameManager.Instance.UpdateScore(-1);
            }
            else
            {
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
            GameManager.Instance.UpdateScore(-1);
        }
    }

    private void YeetItem()
    {
        GameObject itemToYeet = GetClosestItem()?.gameObject;

        catAnimator.SetTrigger("Yeet");

        if (itemToYeet != null)
        {
            int itemScore = itemToYeet.GetComponent<ItemStats>().itemScore;

            if (itemToYeet.tag.Equals("Bad"))
            {
                GameManager.Instance.UpdateScore(itemScore * -1);
            }
            else
            { 
                GameManager.Instance.UpdateScore(itemScore * -1); 
            }

            Destroy(itemToYeet);
        }
        else
        {
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

        // check for closest distance
        foreach (Transform item in allItems)
        {
            float dist = Vector3.Distance(item.position, interactionZone.position);
            if (dist < closestDistance && dist < minInteractionDistance)
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
