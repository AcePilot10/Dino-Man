using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSoundController;

public class DotCollector : MonoBehaviour
{
    private int _dotsLeft; 
    
    private void Start()
    {
        var dots = GameObject.FindGameObjectsWithTag("Dot");
        _dotsLeft = dots.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dot")
        {
            GetComponentInParent<PlayerController>().EatDot(collision.gameObject);
            _dotsLeft--;
            if (_dotsLeft == 0)
            {
                FindObjectOfType<GameManager>().PlayerWon();
            } 
        }
        else if (collision.gameObject.GetComponent<IPowerup>() != null)
        {
            collision.gameObject.GetComponent<IPowerup>().Activate();
            GameSoundController.Instance.PlaySound(GameSoundTypes.LEVEL_UP);
        }
    }
}