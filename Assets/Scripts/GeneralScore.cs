using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GeneralScore : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI boxScoreText;
    public Image productivityBar;
    public GameObject gameOverUI;
    public GameObject playerUI;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "";
        boxScoreText.text = "Score: ";
        UpdateProductivityBar();
    }

    // Update is called once per frame
    void Update()
    {
        boxScoreText.text = "Score: " + GameManager.Instance.GetScore();
        timerText.text = ((int)GameManager.Instance.GetTime()).ToString();
        UpdateProductivityBar();
        GameOver();
    }

    void UpdateProductivityBar()
    {
        productivityBar.fillAmount = (float)GameManager.Instance.GetProductivity() / 100f;
    }

    void GameOver()
    {
        if (GameManager.Instance.GetProductivity() ==  0) 
        {
            Debug.Log("Game over");
            Time.timeScale = 0f;
            Shake.Instance.StopShaking();
            playerUI.SetActive(false);
            gameOverUI.SetActive(true);
            
        }
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
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
}
