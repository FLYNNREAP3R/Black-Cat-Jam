using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralScore : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI boxScoreText;
    public Image productivityBar;
    
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
    }

    void UpdateProductivityBar()
    {
        productivityBar.fillAmount = (float)GameManager.Instance.GetProductivity() / 100f;
    }
}
