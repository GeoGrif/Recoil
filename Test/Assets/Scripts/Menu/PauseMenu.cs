using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public void Retry()
    {
        GameController.isPaused = false;
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("EnemyTest");

    }


}
