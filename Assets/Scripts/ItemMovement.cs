using System;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float itemSpeed = 3.0f;

    public Transform[] path;
    int currentNode;


    void Start()
    {
        
        currentNode = 0;

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        var scaleFactor = width / height / (16f / 9.05f);

        itemSpeed *= Math.Min(1, scaleFactor);

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
            if (gameObject.tag.Equals("Good") || gameObject.tag.Equals("Bad"))
            {
                GameManager.Instance.UpdateScore(-1);
            }

            Destroy(gameObject);
        }

    }

    // Moves gameObject towards next closest node in path
    private void MoveTowardsNode()
    {

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        var scaleFactor = width / height / (16f / 9.05f);

        if (currentNode == 0)
        {
            itemSpeed = 6f * Math.Min(1, scaleFactor);
        } else if (currentNode == 1)
        {
            itemSpeed = 4f * Math.Min(1, scaleFactor);
        } else
        {
            itemSpeed = 2f * Math.Min(1, scaleFactor);
        }

        transform.position = Vector3.MoveTowards(transform.position, path[currentNode].transform.position, itemSpeed * Time.deltaTime);
    }
}
