using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.UI;
using UnityEngine.UI;
using System;

public class Animations_Menu : Animations
{
    public Image fadeImage;
    public float fadeAggregationDuration = 0.1f;

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(nameof(FadeFromBlack));
    }

    private IEnumerator FadeFromBlack()
    {
        while (Math.Round(fadeImage.color.a, 2) != 0.00)
        {
            yield return new WaitForSeconds(fadeAggregationDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - 0.01f);
            if (Math.Round(fadeImage.color.a, 2) == 0.25)
            {
                PlayAnimations();
            }        
        }
        fadeImage.gameObject.SetActive(false);
    }
}