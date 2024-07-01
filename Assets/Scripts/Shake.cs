using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shake : MonoBehaviour
{
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

    public bool start = false;
    public float duration = 1f;
    public AnimationCurve curve;
    public bool canShake = true;

    public Vector3 startPosition;

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

        while (elapsedTime < duration && canShake)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    public void StopShaking()
    {
        canShake = false;
    }
}
