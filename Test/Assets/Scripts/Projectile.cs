using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool checkForMinVelocity = false;
    private Rigidbody2D rb;
    [SerializeField] private float maxVelocity = 50.0f;
    [SerializeField] private float minVelocity = 5.0f;
    [SerializeField] private float timeToDestroy = 1.5f;
    private float tempTimeToDestroy = 1.5f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        tempTimeToDestroy = timeToDestroy;
    }

    void Update()
    {
        //make sure we've achieved more than the min velocity once
        if (!checkForMinVelocity && (rb.velocity.x > minVelocity || rb.velocity.x < -minVelocity || rb.velocity.y > minVelocity || rb.velocity.y < -minVelocity))
        {
            checkForMinVelocity = true;
        }

        if (rb.velocity.x < -maxVelocity)
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


        if (checkForMinVelocity && (rb.velocity.x < minVelocity && rb.velocity.x > -minVelocity) && (rb.velocity.y < minVelocity && rb.velocity.y > -minVelocity))
        {
            tempTimeToDestroy -= Time.deltaTime;
            if (tempTimeToDestroy < 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            tempTimeToDestroy = timeToDestroy;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("a collision has occured ");

        if (collision.collider.gameObject.tag == "projectiles" && collision.otherCollider.gameObject.tag == "projectiles")
        {
            Debug.Log("projectile has hit another projectile ");

            if (checkForMinVelocity && (rb.velocity.x < minVelocity && rb.velocity.x > -minVelocity) && (rb.velocity.y < minVelocity && rb.velocity.y > -minVelocity))
            {
                tempTimeToDestroy -= Time.deltaTime;
                if (tempTimeToDestroy < 0)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                tempTimeToDestroy = timeToDestroy;
            }
        }
    }
}
