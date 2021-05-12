using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyRetreatDuration = 10;

    private bool _isRetreating = false;

    public void StopAllEnemies()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in enemies)
        {
            enemy.isStopped = true;
        }
    }

    public void MakeEnemiesRetreat()
    {
        _isRetreating = true;
        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            enemy.Retreat();
        }
        StartCoroutine(nameof(Retreat));
    }

    private void FinishRetreating()
    {
        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            enemy.FinishRetreating();
        }
        _isRetreating = false;
    }

    private IEnumerator Retreat()
    {
        yield return new WaitForSeconds(enemyRetreatDuration);
        FinishRetreating();
    }

    public bool EnemiesAreRetreating()
    {
        return _isRetreating;
    }
}