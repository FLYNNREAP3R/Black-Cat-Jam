using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AssemblyLine : MonoBehaviour
{

    [SerializeField] private GameObject Test;
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private GameObject ItemContainer;
    [SerializeField] private Transform[] PathNodes;

    [SerializeField] private float minInteractionDistance = 0.5f;
    [SerializeField] private Transform interactionZone;

    [SerializeField] private KeyCode packageKeyCode;
    [SerializeField] private KeyCode yeetKeyCode;

    [SerializeField] private DeliveryTruck deliveryTruck;

    [SerializeField] private TextMeshProUGUI packageKeyText;
    [SerializeField] private TextMeshProUGUI yeetKeyText;

    // Start is called before the first frame update
    void Start()
    {
        packageKeyText.text = packageKeyCode.ToString();
        yeetKeyText.text = yeetKeyCode.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(packageKeyCode))
        {
            PackageItem();
        }

        if (Input.GetKeyDown(yeetKeyCode))
        {
            YeetItem();
        }
    }

    private void PackageItem()
    {
        GameObject itemToPackage = GetClosestItem()?.gameObject;

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

                //Update sprite to package
                itemToPackage.GetComponent<SpriteRenderer>().color = Color.green;
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
        GameObject itemToYeet =GetClosestItem()?.gameObject;

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
        GameObject temp = Instantiate(gameObject, SpawnLocation.position, Quaternion.identity);
        temp.transform.parent = ItemContainer.transform;
        temp.GetComponent<ItemMovement>().path = PathNodes;
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

    // Draw on debug to show interaction distance on assembly line
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(interactionZone.position, minInteractionDistance);
    }
}
