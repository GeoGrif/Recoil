using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
      
    }

    public void Retry()
    {
        GameController.isPaused = false;
        
        Time.timeScale = 1f;

        Debug.Log("The level loder");

        GameObject.FindGameObjectWithTag("GUIController").GetComponent<GUIControllerScript>().CloseMenu();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
