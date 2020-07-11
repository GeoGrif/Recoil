using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    float health = 100;
    Rigidbody2D prb;
    


    // Start is called before the first frame update
    void Start()
    {
        prb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile" && collision.otherCollider.gameObject.name != "Shield")
        {
            health -= 10;
            Debug.Log("the current health is: " + health);
        }
    }

    private void Update()
    {
        if (health < 0)
        {
            Time.timeScale = 0f;
            GameController.isPaused = true;
        }
    }


}
