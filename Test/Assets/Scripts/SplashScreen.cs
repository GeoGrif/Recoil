using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public LevelLoader levelLoader;
    public float timeToSitOnSplashScreen = 5.0f;
    private bool loadMainMenu = false;    

    // Update is called once per frame
    void Update()
    {
        timeToSitOnSplashScreen -= Time.deltaTime;
        if(timeToSitOnSplashScreen < 0)
        {
            loadMainMenu = true;
        }

        if(loadMainMenu)
        {
            levelLoader.FadeToNextLevel();
        }
    }
}
