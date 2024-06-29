using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] Animator transitionAnimation;



    private void Start()
    {
    }

    public void PlayGame()
    {
        levelLoader.LoadNextLevel("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SetActiveTutorial(false);
        SetActiveCredits(false);
        SetActiveSettings(false);
    }

    public void SetActiveTutorial(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        tutorialUI.SetActive(isActive);
    }

    public void SetActiveCredits(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        creditsUI.SetActive(isActive);
    }

    public void SetActiveSettings(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        settingsUI.SetActive(isActive);
    }
}
