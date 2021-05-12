using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public int dotsEaten = 0;

    public void EatDot(GameObject dot)
    {
        dotsEaten++;
        FindObjectOfType<GameManager>().score += 10;
        Destroy(dot);
    }
}
