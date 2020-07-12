using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerScript : MonoBehaviour
{

    public float health = 100;
    Rigidbody2D prb;

    public float score = 0;


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
            score -= 100;
        }
    }

    private void Update()
    {
        if (health < 0)
        {
            Time.timeScale = 0f;
            GameController.isPaused = true;
        }

        score += System.Math.Abs(prb.velocity.x);
        score += System.Math.Abs(prb.velocity.y);
    }

}
