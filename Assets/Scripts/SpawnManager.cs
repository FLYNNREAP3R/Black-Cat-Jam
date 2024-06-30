using System;
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

    [Header("Lists")]
    [SerializeField] private GameObject[] goodItems;
    [SerializeField] private GameObject[] badItems;
    [SerializeField] private AssemblyLine[] assemblyLines;

    [Header("SpawnTimes")]
    [SerializeField] private int currentLevel;
    [SerializeField] private List<SpawnLevelInfo> levels;



    private void Start()
    {
        if (goodItems.Length == 0)
            Debug.LogError("No Good Items Found in Spawn Manager");
        if (badItems.Length == 0)
            Debug.LogError("No Bad Items Found in Spawn Manager");
        if (assemblyLines.Length == 0)
            Debug.LogError("No Assembly Lines Found in Spawn Manager");
        if (levels?.Count == 0)
            Debug.LogError("No Level Info Found in Spawn Manager");

        foreach (AssemblyLine assemblyLineToSpawnAt in assemblyLines) 
        {
            if (assemblyLineToSpawnAt.assemblyLineNumber <= GameSettings.Instance.NumberOfAssemblyLines)
                StartCoroutine(SpawnItems(assemblyLineToSpawnAt));
            else
                assemblyLineToSpawnAt.Disable();
        }

        StartCoroutine(IncreaseLevel());
    }

    private void SpawnGoodItem(AssemblyLine line)
    {
        GameObject itemToSpawn = goodItems[UnityEngine.Random.Range(0, goodItems.Length)];
        line.SpawnItem(itemToSpawn);
    }

    private void SpawnBadItem(AssemblyLine line)
    {
        GameObject itemToSpawn = badItems[UnityEngine.Random.Range(0, badItems.Length)];
        line.SpawnItem(itemToSpawn);
    }

    private IEnumerator SpawnItems(AssemblyLine assemblyLineToSpawnAt)
    {
        while (true)
        {
            float spawnTimer = UnityEngine.Random.Range(levels[currentLevel - 1].timeBetweenSpawnMin, levels[currentLevel - 1].timeBetweenSpawnMax);

            yield return new WaitForSeconds(spawnTimer);

            if (UnityEngine.Random.Range(0f, 1f) <= (levels[currentLevel - 1].goodItemSpawnRate) / 100f)
            {
                SpawnGoodItem(assemblyLineToSpawnAt);
            } else
            {
                SpawnBadItem(assemblyLineToSpawnAt);
            }
        }
    }

    private IEnumerator IncreaseLevel()
    {
        while (currentLevel < levels.Count)
        {
            yield return new WaitForSeconds(levels[currentLevel - 1].timeUntilNextLevel);

            Debug.Log("Increasing Difficulty....");

            currentLevel++;
        }
    }

    [Serializable]
    public struct SpawnLevelInfo
    {
        public float timeBetweenSpawnMin;
        public float timeBetweenSpawnMax;

        public float goodItemSpawnRate;

        public float timeUntilNextLevel;
    }
}
