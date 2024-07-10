using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    [SerializeField] private Vector3 InitialPosition;
    [SerializeField] private Vector3 InitialScale;

    private float previousScaleFactor;

    // Start is called before the first frame update
    void Start()
    {

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        previousScaleFactor = width / height / (16f / 9.05f);

        Scale(previousScaleFactor);

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        var scaleFactor = width / height / (16f / 9.05f);

        if(scaleFactor != previousScaleFactor) {

            Scale(scaleFactor);

            previousScaleFactor = scaleFactor;

        }

    }

    private void Scale(float scaleFactor) {

        var newLocalScale = new Vector3(
            InitialScale.x > 0 ? Math.Min(InitialScale.x, InitialScale.x * scaleFactor) : Math.Max(InitialScale.x, InitialScale.x * scaleFactor),
            InitialScale.y > 0 ? Math.Min(InitialScale.y, InitialScale.y * scaleFactor) : Math.Max(InitialScale.y, InitialScale.y * scaleFactor),
            InitialScale.z > 0 ? Math.Min(InitialScale.z, InitialScale.z * scaleFactor) : Math.Max(InitialScale.z, InitialScale.z * scaleFactor)
        );
        transform.localScale = newLocalScale;

        if(tag != "Bad" && tag != "Good" && tag != "Package") {
            var newPosition = new Vector3(
                InitialPosition.x > 0 ? Math.Min(InitialPosition.x, InitialPosition.x * scaleFactor) : Math.Max(InitialPosition.x, InitialPosition.x * scaleFactor),
                InitialPosition.y > 0 ? Math.Min(InitialPosition.y, InitialPosition.y * scaleFactor) : Math.Max(InitialPosition.y, InitialPosition.y * scaleFactor),
                InitialPosition.z > 0 ? Math.Min(InitialPosition.z, InitialPosition.z * scaleFactor) : Math.Max(InitialPosition.z, InitialPosition.z * scaleFactor)
            );
            transform.position = newPosition;
        }

    }
}
