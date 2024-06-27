using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruck : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadedBoxes;
    [SerializeField] private List<GameObject> unloadedBoxes;

    private void Start()
    {
        if (unloadedBoxes == null)
        {
            Debug.LogError("Boxes are not set in DeliverTruck.cs");
        }

        UnloadAllBoxes();
    }

    public void LoadBox()
    {
        if (unloadedBoxes.Count <= 0)
        {
            PlayDriveOffAnimation();
            UnloadAllBoxes();
            return;
        }

        int randomIndex = Random.Range(0, unloadedBoxes.Count);

        GameObject boxToLoad = unloadedBoxes[randomIndex];
        unloadedBoxes.Remove(boxToLoad);
        loadedBoxes.Add(boxToLoad);
        boxToLoad.gameObject.SetActive(true);
    }

    public void UnloadAllBoxes()
    {

        unloadedBoxes.AddRange(loadedBoxes);
        loadedBoxes.RemoveAll(s => s != null);
        foreach (GameObject box in unloadedBoxes)
        {
            box.gameObject.SetActive(false);
        }
    }

    public void PlayAngryCatAnimation()
    {

    }

    public void PlayDriveOffAnimation()
    {

    }
}
