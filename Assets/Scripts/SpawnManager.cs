using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region
    //singleton
    public static SpawnManager Instance { set; get; }
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

    [SerializeField] private GameObject[] goodItems;
    [SerializeField] private GameObject[] badItems;
    [SerializeField] private AssemblyLine[] assemblyLines;

    [SerializeField] private float timeBetweenSpawnMin = 1;
    [SerializeField] private float timeBetweenSpawnMax = 3;

    private void Start()
    {
        if (goodItems.Length == 0)
            Debug.LogError("No Good Items Found in Spawn Manager");
        if (badItems.Length == 0)
            Debug.LogError("No Bad Items Found in Spawn Manager");
        if (assemblyLines.Length == 0)
            Debug.LogError("No Assembly Lines Found in Spawn Manager");
        
        foreach (AssemblyLine assemblyLineToSpawnAt in assemblyLines) 
        {
            if (assemblyLineToSpawnAt.assemblyLineNumber <= GameSettings.Instance.NumberOfAssemblyLines)
                StartCoroutine(SpawnItems(assemblyLineToSpawnAt));
            else
                assemblyLineToSpawnAt.Disable();
        }
    }

    private void SpawnGoodItem(AssemblyLine line)
    {
        GameObject itemToSpawn = goodItems[Random.Range(0, goodItems.Length)];
        line.SpawnItem(itemToSpawn);
    }

    private void SpawnBadItem(AssemblyLine line)
    {
        GameObject itemToSpawn = badItems[Random.Range(0, goodItems.Length)];
        line.SpawnItem(itemToSpawn);
    }

    private IEnumerator SpawnItems(AssemblyLine assemblyLineToSpawnAt)
    {
        while (true)
        {
            float spawnTimer = Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax);

            yield return new WaitForSeconds(spawnTimer);

            if (Random.Range(0f, 1f) > 0.5)
            {
                SpawnGoodItem(assemblyLineToSpawnAt);
            } else
            {
                SpawnBadItem(assemblyLineToSpawnAt);
            }
        }
    }
}
