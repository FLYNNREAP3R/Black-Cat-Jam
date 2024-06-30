using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;

    public void Retry()
    {
        levelLoader.LoadNextLevel("SampleScene");
    }

    public void MainMenu()
    {
        levelLoader.LoadNextLevel("MainMenu");
    }
}
