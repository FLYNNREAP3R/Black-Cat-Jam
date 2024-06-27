using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;

    private void Start()
    {
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SetActiveTutorial(bool isActive)
    {
        tutorialUI.SetActive(isActive);
    }
}
