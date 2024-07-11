using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shake : MonoBehaviour
{

    public bool start = false;
    public float duration = 1f;
    public AnimationCurve curve;
    public bool canShake = true;

    public Vector3 mainCameraStartPosition;
    private Vector3 productivityCasingStartPosition;
    private Vector3 productivityBarBgStartPosition;

    public GameObject productivityBarBg;
    public GameObject productivityCasing;

    #region
    //singleton
    public static Shake Instance { set; get; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //singleton
    #endregion

    private void Update()
    {
        canShake = GameSettings.Instance.canShake;

        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    private IEnumerator Shaking()
    {

        float elapsedTime = 0f;

        productivityBarBgStartPosition = new(
            productivityBarBg.transform.position.x,
            productivityBarBg.transform.position.y,
            productivityBarBg.transform.position.z
        );
        productivityCasingStartPosition = new(
            productivityCasing.transform.position.x,
            productivityCasing.transform.position.y,
            productivityCasing.transform.position.z
        );

        Vector2 topRightCorner = new(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        var width = edgeVector.x * 2;

        var scaleFactor = System.Math.Min(1, width / height / (16f / 9.05f));

        while (elapsedTime < duration && canShake)
        {
            var randomNumber = Random.insideUnitSphere;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = mainCameraStartPosition + randomNumber * strength * scaleFactor;
            productivityBarBg.transform.position = productivityBarBgStartPosition + randomNumber * strength * 20 * scaleFactor;
            productivityCasing.transform.position = productivityCasingStartPosition + randomNumber * strength * 20 * scaleFactor;
            yield return null;
        }

        transform.position = mainCameraStartPosition;
        productivityBarBg.transform.position = productivityBarBgStartPosition;
        productivityCasing.transform.position = productivityCasingStartPosition;

    }

    public void StopShaking()
    {
        canShake = false;
    }
}
