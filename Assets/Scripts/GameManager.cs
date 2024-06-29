using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Cat Reaction Variables
    [SerializeField] private SpriteRenderer CatSpriteRenderer;
    [SerializeField] private List<Sprite> CatReactionSprites = new List<Sprite>();

    [SerializeField] private LevelLoader levelLoader;

    // Multiplier Fields
    private int currentMultiplerIndex = 0;
    private int actionsTilMultIncrease;
    private int maxActionsTilMultIncrease = 5;
    private int[] multiplierLevels = { 1, 2, 4, 8, 16 };

    // Player Life
    [Range(0, 100)] private int productivity = 50;
    private int consecutiveFuckUps = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        boxScore = 0;
        actionsTilMultIncrease = maxActionsTilMultIncrease;
        productivity = 50;
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

    public int GetMult()
    {
        return multiplierLevels[currentMultiplerIndex];
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
            productivity = Mathf.Clamp(productivity + (scoreAdjustment * consecutiveFuckUps * 2), 0, 100);

            currentMultiplerIndex = 0;
            actionsTilMultIncrease = maxActionsTilMultIncrease;

            // Check if Game Over
            if (productivity <= 0)
            {
                Time.timeScale = 0f;
                Shake.Instance.StopShaking();
                UiManager.Instance.GameOver();
            }
        }
        // Good Score
        else
        {
            consecutiveFuckUps = 0;
            productivity = Mathf.Clamp(productivity + (scoreAdjustment * multiplierLevels[currentMultiplerIndex] * 2), 0, 100);
            actionsTilMultIncrease--;
            if (actionsTilMultIncrease <= 0)
            {
                actionsTilMultIncrease = maxActionsTilMultIncrease;
                currentMultiplerIndex++;
            }
            boxScore += (scoreAdjustment * multiplierLevels[currentMultiplerIndex]);
        }

        UpdateCatReaction();
    }

    // Scene Management
    public void RetryGame()
    {
        levelLoader.LoadNextLevel("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        levelLoader.LoadNextLevel("MainMenu");
    }

    private void UpdateCatReaction()
    {
        switch (productivity)
        {
            case > 75:
                CatSpriteRenderer.sprite = CatReactionSprites[0];
                break;
            case > 50:
                CatSpriteRenderer.sprite = CatReactionSprites[1];
                break;
            case > 25:
                CatSpriteRenderer.sprite = CatReactionSprites[2];
                break;
            default:
                CatSpriteRenderer.sprite = CatReactionSprites[3];
                break;
        }
    }

}
