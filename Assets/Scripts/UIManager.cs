using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image hpDisplay;
    public GameObject titleScreen;

    public Text scoreText;
    public int score;

    public void UpdateHP(int currentHP)
    {
        hpDisplay.sprite = lives[currentHP];
    }
    
    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
