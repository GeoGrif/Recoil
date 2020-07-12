using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Tooltip("Use File>build settings for each scene's index")]
    public int levelIndex = 0;

    private BoxCollider2D player;
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider == player)
        {
            if (levelIndex > 0)
            {
                levelLoader.FadeToLevel(levelIndex);
            }
            else if(SceneManager.GetActiveScene().buildIndex + 1 != null)
            {
                levelLoader.FadeToNextLevel();
            }
            else
            {
                //default to main menu if the levelindex doesn't exist
                Debug.Log("Ensure that the level index exists in file>build settings");
                levelLoader.FadeToLevel(1);
            }
        }
    }
}
