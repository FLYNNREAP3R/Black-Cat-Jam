using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    public Image productivityBar;
    public GameObject gameOverUI;
    public GameObject playerUI;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> numbers;

    [SerializeField] private SpriteRenderer FirstDigitScrore;
    [SerializeField] private SpriteRenderer SecondDigitScore;
    [SerializeField] private SpriteRenderer ThirdDigitScore;
    [SerializeField] private SpriteRenderer FourthDigitScore;

    [SerializeField] private SpriteRenderer FirstMultScore;
    [SerializeField] private SpriteRenderer SecondMultScore;



    // Start is called before the first frame update
    void Start()
    {
        playerUI.SetActive(true);
        gameOverUI.SetActive(false);
        UpdateProductivityBar();
    }

    // Update is called once per frame
    void Update()
    {
        var boxScore = GameManager.Instance.GetScore();
        var multScore = GameManager.Instance.GetMult();

        UpdateScore(boxScore);
        UpdateMult(multScore);
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

    private void UpdateDigit(int number, SpriteRenderer renderer)
    {
        renderer.sprite = numbers[number];
    }

    private void UpdateScore(int boxScore)
    {
        var firstNumberScore = boxScore % 10;
        UpdateDigit(firstNumberScore, FirstDigitScrore);

        var secondNumberScore = (boxScore % 100) / 10;
        UpdateDigit(secondNumberScore, SecondDigitScore);

        var thirdNumberScore = (boxScore % 1000) / 100;
        UpdateDigit(thirdNumberScore, ThirdDigitScore);

        var fourthNumberScore = (boxScore % 10000) / 1000;
        UpdateDigit(fourthNumberScore, FourthDigitScore);
    }

    private void UpdateMult(int multScore)
    {
        var firstNumberMult = multScore % 10;
        UpdateDigit(firstNumberMult, FirstMultScore);

        var secondNumberMult = (multScore % 100) / 10;
        UpdateDigit(secondNumberMult, SecondMultScore);
    }
}
