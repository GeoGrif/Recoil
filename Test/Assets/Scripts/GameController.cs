using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public int level = 1;

    private static GameController privateInstance;

    public static GameController instance
    {
        get
        {
            if (privateInstance == null)
            {
                //if there is no instance of GameController, create one if/when it's called in script
                privateInstance = FindObjectOfType<GameController>();
                if (privateInstance == null)
                {
                    privateInstance = new GameObject("Created Game Controller", typeof(GameController)).GetComponent<GameController>();
                }
            }

            return privateInstance;
        }
        private set
        {
            privateInstance = value;
        }
    }

    void Awake()
    {
        //make sure we don't destroy this instance
        DontDestroyOnLoad(this.gameObject);
    }
}
