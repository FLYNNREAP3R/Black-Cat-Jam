using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject settingsUI;

    [SerializeField] Animator transitionAnimation;

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

    public void SetActiveCredits(bool isActive)
    {
        creditsUI.SetActive(isActive);
    }

    public void SetActiveSettings(bool isActive)
    {
        settingsUI.SetActive(isActive);
    }
}
