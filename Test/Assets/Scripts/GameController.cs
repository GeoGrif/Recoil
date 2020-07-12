using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] public GameObject UI;


    [SerializeField] public GameObject pauseMenu;

    [SerializeField] public GameObject HUD;
    
    [HideInInspector] public static bool isPaused = false;

    [HideInInspector] public int level = 1;

    private static float OriginalTimeScale;


    private void Start()
    {
     
    }

    private void Update()
    {
    }



}
