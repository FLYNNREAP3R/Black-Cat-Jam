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

    // Checks to see if gameObject has reached last node. If true, gameObject is destroyed
    private void CheckMoveToNextNode()
    {
        bool hasReachedNode = Vector3.Distance(transform.position, path[currentNode].transform.position) < 0.1;

        if (hasReachedNode && currentNode < path.Length - 1)
        {
            // Move to next node
            currentNode++;
        }
        else if (hasReachedNode)
        {
            // TODO -> Need to update to check if item was packaged/yeeted correctly. Currently adds 1 if item passes through last node
            if (gameObject.tag.Equals("Good") || gameObject.tag.Equals("Bad"))
            {
                GameManager.Instance.UpdateScore(1);
            }

                
            Destroy(gameObject);
        }

    }

    // Moves gameObject towards next closest node in path
    private void MoveTowardsNode()
    {
        transform.position = Vector3.MoveTowards(transform.position, path[currentNode].transform.position, itemSpeed * Time.deltaTime);
    }
}