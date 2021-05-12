using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesText : TextBase
{
    public override void UpdateText()
    {
        var lives = FindObjectOfType<GameManager>().lives;
        if (lives == -1)
            lives = 0;
        _text.text = "Lives: " + lives;
    }
}