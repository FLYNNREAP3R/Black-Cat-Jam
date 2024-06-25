using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float itemSpeed = 5.0f;

    public Transform[] path;
    int currentNode;


    void Start()
    {
        currentNode = 0;
    }

    void Update()
    {
        CheckMoveToNextNode();
        MoveTowardsNode();
    }

    private void CheckMoveToNextNode()
    {
        if (Vector3.Distance(transform.position, path[currentNode].transform.position) < 0.1 && currentNode < path.Length - 1)
        {
            // Move to next node
            currentNode++;
        }
    }

    private void MoveTowardsNode()
    {
        transform.position = Vector3.MoveTowards(transform.position, path[currentNode].transform.position, itemSpeed * Time.deltaTime);
    }
}
