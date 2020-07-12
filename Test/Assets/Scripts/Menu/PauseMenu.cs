using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void Retry()
    {
        GameController.isPaused = false;
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("EnemyTest");

    }

}
