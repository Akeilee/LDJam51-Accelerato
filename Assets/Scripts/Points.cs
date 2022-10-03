using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [SerializeField] private Sprite [] numbers;

    [Header("Score")]
    [SerializeField] private Image leftScore;
    [SerializeField] private Image middleScore;
    [SerializeField] private Image rightScore;

    [SerializeField] private Image leftBigScore;
    [SerializeField] private Image middleBigScore;
    [SerializeField] private Image rightBigScore;

    [Header("Time")]
    [SerializeField] private Image leftTime;
    [SerializeField] private Image middleTime;
    [SerializeField] private Image rightTime;

    [SerializeField] private Image leftBigTime;
    [SerializeField] private Image middleBigTime;
    [SerializeField] private Image rightBigTime;

    [Header("Cheese")]
    [SerializeField] private Image leftCheese;
    [SerializeField] private Image middleCheese;

    [SerializeField] private Image leftBigCheese;
    [SerializeField] private Image middleBigCheese;

    [Header("Sheets")]
    [SerializeField] private Image leftSheet;
    [SerializeField] private Image middleSheet;

    [SerializeField] private Image leftBigSheet;
    [SerializeField] private Image middleBigSheet;


    private decimal pointsTruncate;
    private decimal firstDecimalPlace;
    private decimal secondDecimalPlace;


    private void Start()
    {
        leftBigScore.gameObject.SetActive(true);
        leftScore.gameObject.SetActive(true);
    }

    private void Update()
    {
    }

    public void UpdateScoreTime(int points, Image left, Image middle, Image right) 
    {
        if (points < 10)
        {
            middle.gameObject.SetActive(false);

            if(right!= null)
                right.gameObject.SetActive(false);

            left.sprite = numbers[points];
        }
        else if (points >= 10 && points <= 99)
        {
            middle.gameObject.SetActive(true);

            pointsTruncate = (decimal)(points / 10.0f);
            firstDecimalPlace = (int)((pointsTruncate % 1) * 10);

            middle.sprite = numbers[(int)firstDecimalPlace];
            left.sprite = numbers[(int)decimal.Truncate(pointsTruncate)];
        }
        else if (points > 99) 
        {
            pointsTruncate = (decimal)(points / 10.0f);
            secondDecimalPlace = (int)((pointsTruncate % 1) * 10);

            pointsTruncate = (decimal)(points / 100.0f);
            firstDecimalPlace = (int)((pointsTruncate % 1) * 10);

            if (right != null) 
            {
                right.gameObject.SetActive(true);
                right.sprite = numbers[(int)secondDecimalPlace];
                middle.sprite = numbers[(int)firstDecimalPlace];
                left.sprite = numbers[(int)decimal.Truncate(pointsTruncate)];
            }
        }
    }

    public void UpdateScoreBig(int points) 
    {
        UpdateScoreTime(points, leftBigScore, middleBigScore, rightBigScore);
    }

    public void UpdateScoreSmall(int points) 
    {
        UpdateScoreTime(points, leftScore, middleScore, rightScore);
    }

    public void UpdateTimeBig(int time)
    {
        UpdateScoreTime(time, leftBigTime, middleBigTime, rightBigTime);
    }

    public void UpdateTimeSmall(int time)
    {
        UpdateScoreTime(time, leftTime, middleTime, rightTime);
    }

    public void UpdateCheeseBig(int cheese)
    {
        UpdateScoreTime(cheese, leftBigCheese, middleBigCheese, null);
    }

    public void UpdateCheeseSmall(int cheese)
    {
        UpdateScoreTime(cheese, leftCheese, middleCheese, null);
    }

    public void UpdateSheetsBig(int sheets)
    {
        UpdateScoreTime(sheets, leftBigSheet, middleBigSheet, null);
    }

    public void UpdateSheetsSmall(int sheets)
    {
        UpdateScoreTime(sheets, leftSheet, middleSheet, null);
    }

}
