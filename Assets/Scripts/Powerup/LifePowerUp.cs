using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUp : PowerupBase
{
    public override void Activate()
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager.lives < 3) { 
            gameManager.lives++;
        }
        gameManager.score += 25;
        base.Activate();
    }
}