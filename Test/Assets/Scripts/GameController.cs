using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    
    [HideInInspector] public static bool isPaused = false;

    [HideInInspector] public int level = 1;

    private static GameController privateInstance;

    private List<GameObject> _Projectiles;
   


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

    private GameController() 
    {
        _Projectiles = new List<GameObject>();
    }

    /**
    * Add a new projectile to the set of projectiles currently in play. 
    */
    public void AddProjectile(GameObject projectile)
    {
        _Projectiles.Add(projectile);
    }

    /**
     * Add a new projectile to the set of projectiles currently in play. 
    */  
    public void RemoveProjectile(GameObject projectile) 
    {
        _Projectiles.Remove(projectile);
    }


    void Awake()
    {
        //make sure we don't destroy this instance
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (isPaused)
        {
            UI.SetActive(true);
        }   
        else
        {
            UI.SetActive(false);
        }
    }
}
