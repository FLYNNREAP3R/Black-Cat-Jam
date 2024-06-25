using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToSpawn;
    [SerializeField] private AssemblyLine[] assemblyLines;

    private void Start()
    {
        assemblyLines[0].SpawnItem(itemsToSpawn[0]);
    }


    // Spawn Good Item

    // Spawn Bad Item
}
