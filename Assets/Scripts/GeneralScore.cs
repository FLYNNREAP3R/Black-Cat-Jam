using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralScore : MonoBehaviour
{
    [SerializeField] private float timeLeft;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI boxScoreTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "";
        boxScoreTxt.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        boxScoreTxt.text = "Score: " + GameManager.Instance.GetScore();

        if (timeLeft > 0) 
        {
            Debug.Log("Keep going!");
            timeLeft -= Time.deltaTime;
            UpdateTImer(timeLeft);
        }
        else
        {
            Debug.Log("Out of time!");
            timeLeft = 0;
        }
    }

    void UpdateTImer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
