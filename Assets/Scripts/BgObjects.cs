using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BgObjects : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private List<GameObject> rightChildren;
    private List<GameObject> leftChildren;

    [SerializeField] private GameObject background;

    [SerializeField] private List<Sprite> sprites;

    private void Start()
    {
        rightChildren = new List<GameObject>();

        Transform[] rightChildrenTransforms = GameObject.Find("Right").GetComponentsInChildren<Transform>();

        foreach (Transform childTransform in rightChildrenTransforms)
        {
            rightChildren.Add(childTransform.gameObject);
            // Give Random Sprite
        }

        leftChildren = new List<GameObject>();

        Transform[] leftChildrenTransforms = GameObject.Find("Left").GetComponentsInChildren<Transform>();

        foreach (Transform childTransform in leftChildrenTransforms)
        {
            leftChildren.Add(childTransform.gameObject);
            // Give Random Sprite
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject child in rightChildren) 
        {
            if (child.transform.position.x > 11)
            {
                child.transform.position = new Vector3(-12, child.transform.position.y, child.transform.position.z);
            } else
            {
                child.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }
        }

        foreach (GameObject child in leftChildren)
        {
            if (child.transform.position.x < -12)
            {
                child.transform.position = new Vector3(11, child.transform.position.y, child.transform.position.z);
            }
            else
            {
                child.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
