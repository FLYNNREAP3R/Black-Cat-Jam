using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private int boxScore = 0;

    public int GetScore()
    {
        return boxScore;
    }

    public void UpdateScore(int scoreAdjustment)
    {
        boxScore += scoreAdjustment;
    }

    // Start is called before the first frame update
    void Start()
    {
        boxScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
