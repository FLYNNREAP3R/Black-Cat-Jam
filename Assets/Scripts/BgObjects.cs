using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BgObjects : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isRight;

    private void FixedUpdate()
    {
        if (isRight) {
            if (transform.position.x > 11)
            {
                transform.position = new Vector3(-12, transform.position.y, transform.position.z);
            } else
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (transform.position.x < -12)
            {
                transform.position = new Vector3(11, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
