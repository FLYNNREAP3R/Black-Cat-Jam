using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn An Item
    public void SpawnItem(GameObject gameObject)
    {
        GameObject temp = Instantiate(gameObject, SpawnLocation.position, Quaternion.identity);
        temp.transform.parent = ItemContainer.transform;
        temp.GetComponent<ItemMovement>().path = PathNodes;
    }

    private Transform GetClosetItem()
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
