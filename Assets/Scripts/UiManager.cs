using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region
    //singleton
    public static UiManager Instance { set; get; }
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

    public TextMeshProUGUI boxScoreText;
    public TextMeshProUGUI multText;
    public Image productivityBar;
    public GameObject gameOverUI;
    public GameObject playerUI;
    
    // Start is called before the first frame update
    void Start()
    {
        playerUI.SetActive(true);
        gameOverUI.SetActive(false);
        boxScoreText.text = "0000";
        multText.text = "01";
        UpdateProductivityBar();
    }

    // Update is called once per frame
    void Update()
    {
        var boxScore = GameManager.Instance.GetScore().ToString();
        var multScore = GameManager.Instance.GetMult().ToString();

        while (boxScore.Length < 4)
        {
            boxScore = "0" + boxScore;
        }

        while (multScore.Length < 2)
        {
            multScore = "0" + multScore;
        }

        boxScoreText.text = boxScore;
        multText.text = multScore;
        UpdateProductivityBar();
    }

    void UpdateProductivityBar()
    {
        productivityBar.fillAmount = (float)GameManager.Instance.GetProductivity() / 100f;
    }

    public void GameOver()
    {
        playerUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
