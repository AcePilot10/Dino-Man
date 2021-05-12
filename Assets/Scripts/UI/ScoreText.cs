using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : TextBase
{
    public override void UpdateText()
    {
        var currentScore = FindObjectOfType<GameManager>().score;
        string scoreFormatted = currentScore.ToString().PadLeft(3, '0');
        _text.text = "Score: " + scoreFormatted;
    }
}