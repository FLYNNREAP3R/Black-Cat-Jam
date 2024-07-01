using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PreviousScore : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private List<Sprite> numbers;

    [SerializeField] private SpriteRenderer FirstDigitScrore;
    [SerializeField] private SpriteRenderer SecondDigitScore;
    [SerializeField] private SpriteRenderer ThirdDigitScore;
    [SerializeField] private SpriteRenderer FourthDigitScore;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(GameSettings.Instance.previousScore);
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
}
