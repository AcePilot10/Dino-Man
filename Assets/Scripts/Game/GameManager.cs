using Assets.Scripts.SFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    public int lives = 3;

    private AudioSource playerMoveSound;

    private void Start()
    {
        playerMoveSound = GameObject.FindGameObjectWithTag("Player Move Sound").GetComponent<AudioSource>();
    }

    public void PlayerHit(EnemyMovement enemy)
    {
        if (FindObjectOfType<PlayerController>().IsDying)
            return;
        if (!FindObjectOfType<EnemyManager>().EnemiesAreRetreating())
        {
            var player = FindObjectOfType<PlayerController>();
            var enemyManager = FindObjectOfType<EnemyManager>();
            if (player.IsDying)
                return;
            player.Die();
            lives--;
            enemyManager.StopAllEnemies();
            playerMoveSound.Stop();
        }
        else
        {
            GameSoundController.Instance.PlaySound(GameSoundController.GameSoundTypes.EAT);
            enemy.ReturnToPrison();
            score += 75;
        }
    }

    public void Respawn()
    {
        if (lives == -1)
            PlayerLost();
        else
        {
            foreach (var enemy in FindObjectsOfType<EnemyMovement>())
            {
                enemy.Reset();
            }
            var player = FindObjectOfType<PlayerController>();
            player.Reset();
            player.IsDying = false;
            playerMoveSound.Play();
        }
    }

    public void PlayerLost()
    {
        SceneManager.LoadSceneAsync("Game Over - Lost");
    }

    public void PlayerWon()
    {
        SceneManager.LoadSceneAsync("Game Over - Won");
    }
}