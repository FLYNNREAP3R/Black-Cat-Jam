using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region
    //singleton
    public static GameManager Instance { set; get; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //singleton
    #endregion

    private int boxScore = 0;
    private float timeLeft = 0f;

    // Multiplier Fields
    private int currentMultiplerIndex = 0;
    private int actionsTilMultIncrease;
    private int maxActionsTilMultIncrease = 5;
    private int[] multiplierLevels = { 1, 2, 4, 8 };

    // Player Life
    [Range(0, 100)] private int productivity = 50;
    private int consecutiveFuckUps = 0;

    // Start is called before the first frame update
    void Start()
    {
        boxScore = 0;
        actionsTilMultIncrease = maxActionsTilMultIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
    }

    public int GetScore()
    {
        return boxScore;
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public int GetProductivity()
    {
        return productivity;
    }

    public void UpdateScore(int scoreAdjustment)
    {
        // Bad Score
        if (scoreAdjustment < 0)
        {
            Shake.Instance.start = true;

            consecutiveFuckUps++;
            productivity += scoreAdjustment * consecutiveFuckUps * 2;

            currentMultiplerIndex = 0;
            actionsTilMultIncrease = maxActionsTilMultIncrease;
        }
        // Good Score
        else
        {
            consecutiveFuckUps = 0;
            productivity += scoreAdjustment * multiplierLevels[currentMultiplerIndex] * 2;
            actionsTilMultIncrease--;
            if (actionsTilMultIncrease <= 0)
            {
                actionsTilMultIncrease = maxActionsTilMultIncrease;
                currentMultiplerIndex++;
                Debug.Log("Multiplier Increased to " + multiplierLevels[currentMultiplerIndex]);
            }
            boxScore += (scoreAdjustment * multiplierLevels[currentMultiplerIndex]);
        }
    }
}
