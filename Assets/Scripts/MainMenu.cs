using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject difficultyUI;

    [SerializeField] private GameObject mainMenuBackground;
    [SerializeField] private GameObject tutorialBackground;
    [SerializeField] private GameObject creditsBackground;
    [SerializeField] private GameObject settingsBackground;
    [SerializeField] private GameObject difficultyBackground;
    
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Toggle screenShakeToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] Animator transitionAnimation;

    public void PlayGame()
    {
        SetActiveDifficulty(true);
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
        mainMenuBackground.SetActive(!isActive);
        tutorialUI.SetActive(isActive);
        tutorialBackground.SetActive(isActive);
    }

    public void SetActiveCredits(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        mainMenuBackground.SetActive(!isActive);
        creditsUI.SetActive(isActive);
        creditsBackground.SetActive(isActive);
    }

    public void SetActiveSettings(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        mainMenuBackground.SetActive(!isActive);
        settingsUI.SetActive(isActive);
        settingsBackground.SetActive(isActive);
    }

    public void SetActiveDifficulty(bool isActive)
    {
        mainMenuUI.SetActive(!isActive);
        mainMenuBackground.SetActive(!isActive);
        tutorialUI.SetActive(!isActive);
        tutorialBackground.SetActive(!isActive);
        difficultyUI.SetActive(isActive);
        difficultyBackground.SetActive(isActive);
    }

    public void UpdateScreenShake()
    {
        GameSettings.Instance.canShake = !GameSettings.Instance.canShake;
    }

    public void UpdateVolume()
    {
        GameSettings.Instance.volume = (int)(volumeSlider.value * 100);
        audioManager.ChangeVolume();
    }

    public void DebugTest()
    {
        Debug.Log("This is a test message!");
    }

    public void AssemblyLineOne()
    {
        GameSettings.Instance.NumberOfAssemblyLines = 1;
        levelLoader.LoadNextLevel("SampleScene");
    }
    public void AssemblyLineTwo()
    {
        GameSettings.Instance.NumberOfAssemblyLines = 2;
        levelLoader.LoadNextLevel("SampleScene");
    }
    public void AssemblyLineThree()
    {
        GameSettings.Instance.NumberOfAssemblyLines = 3;
        levelLoader.LoadNextLevel("SampleScene");
    }
    public void AssemblyLineFour()
    {
        GameSettings.Instance.NumberOfAssemblyLines = 4;
        levelLoader.LoadNextLevel("SampleScene");
    }


}
