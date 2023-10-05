using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int score = 0;
    TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
}
