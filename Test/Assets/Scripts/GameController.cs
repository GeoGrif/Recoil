using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private GameObject UI;
    
    [HideInInspector] public static bool isPaused = false;

    [HideInInspector] public int level = 1;

    private List<GameObject> _Projectiles;


    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("Canvas");
        
        if(UI == null)Debug.Log("CANVAS");
    }


    private void Update()
    {
        if (isPaused)
        {
            Debug.Log("is paused");

            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }
}
