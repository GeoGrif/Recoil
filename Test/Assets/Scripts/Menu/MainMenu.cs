using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void PlayGame()
    {
        //load the next scene from build index - set this in file>build settings
        levelLoader.FadeToNextLevel();
    }

    public void Quit()
    {
        Debug.Log("Pressed quit");
        Application.Quit();
    }
}
