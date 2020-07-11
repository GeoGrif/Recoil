using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float maxVelocity = 50.0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(rb.velocity.x < -maxVelocity)
        {
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
        }

        if (rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        }

        if (rb.velocity.y < -maxVelocity)
        {               
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
        }               
                        
        if (rb.velocity.y > maxVelocity)
        {               
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
        }
    }


}
