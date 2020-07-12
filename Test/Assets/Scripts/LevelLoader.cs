using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public static bool loadingNextLevel = false;



    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        loadingNextLevel = true;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToNextLevel()
    {
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        loadingNextLevel = true;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        loadingNextLevel = false;
    }
}
