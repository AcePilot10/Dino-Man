using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public ButtonAction[] buttonActions;
    
    void Start()
    {
        foreach (var button in buttonActions)
        {
            button.button.onClick.AddListener(() => SceneManager.LoadSceneAsync(button.scene));
        }
    }
}