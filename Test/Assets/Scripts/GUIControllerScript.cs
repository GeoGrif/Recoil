using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControllerScript : MonoBehaviour
{
    [SerializeField] public GameObject UI;

    [SerializeField] public GameObject pauseMenu;

    [SerializeField] public GameObject HUD;

    [HideInInspector] public static bool isPaused = false;

    private static float OriginalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(true);
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();

        }
    }

    public void SetPause(bool paused)
    {
        isPaused = paused;

        GameController.isPaused = paused;

        if (isPaused)
        {
            OriginalTimeScale = Time.timeScale;

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void OpenMenu()
    {
        if (UI != null)
        {
            
            HUD.SetActive(false);
            pauseMenu.SetActive(true);
            SetPause(true);
        }
    }

    public void CloseMenu()
    {
        if (UI != null)
        {
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
            SetPause(false);
        }
    }

}
