using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
     
    public void Start()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void PlayGame()
    {
        
    }
    public void ExitOnClick()
    {
        Application.Quit();
    }
    public void menuselection(string buttonlabel)
    {
        if (buttonlabel.Contains("Back") == true)
        {
            OptionsMenu.SetActive(false);
            MainMenu.SetActive(true);
        } else if (buttonlabel.Contains("Options") == true)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
            
    }
}
