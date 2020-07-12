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
    [SerializeField] private float ExplosiveRange = 0.1f;
    [SerializeField] private float ExplosivePower = 10f;
    [SerializeField] public float Damage = 10.0f;
    [SerializeField] private float force = 100.0f;

    private int numberOfBounces = 0;
    [SerializeField] private int maxNumberOfBounces = 10;

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip ricochetSound;

    private float tempTimeToDestroy = 1.5f;

    private bool isVisible;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        tempTimeToDestroy = timeToDestroy;
    }

    void Update()
    {
        transform.up = rb.velocity;

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

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        numberOfBounces++;
        if(numberOfBounces >= maxNumberOfBounces)
        {
            Destroy(this.gameObject);
        }
        //if there is a collision and it's with this object. 
        if (collision.collider.gameObject.tag == "Projectile" && collision.otherCollider.gameObject.tag == "Projectile")
        {

            Rigidbody2D rb;

            Vector2 dir = new Vector2(collision.contacts[0].point.x - transform.position.x, collision.contacts[0].point.y - transform.position.y);
            dir = dir.normalized;

            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * force);

            if (isVisible)
            {
                AudioManager.instance.PlaySFX(ricochetSound);
            }

            //Vector3 explosionPos = transform.position;

            //CameraScript.TriggerShake();
            //AudioManager.instance.PlaySFX(explosionSound);

            //Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, ExplosiveRange);

            //foreach (Collider2D hit in colliders)
            //{
            //    Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            //    if (rb != null) 
            //    {
            //        Vector3 hitPosition = rb.position;

            //        var explosionDir = hitPosition - explosionPos;
            //        var explosionDistance = explosionDir.magnitude;

            //        explosionDistance /= ExplosiveRange;

            //        explosionDir.Normalize();

            //        float force = Mathf.Lerp(0, ExplosivePower, (1 - explosionDistance));

            //        Vector3 forceDirection = force * explosionDir;

            //        //Debug.Log("Evplosive force " + force);
            //        //Debug.Log("explosionDistance " + explosionDistance);
            //        //Debug.Log("Evplosive direction vector " + forceDirection);
            //        //Debug.Log("explosing on object: " + rb.gameObject.name);

            //        rb.AddForce(forceDirection, ForceMode2D.Impulse);
            //    }

            //}

        }
        else if(collision.collider.gameObject.name != "Shield" || collision.collider.gameObject.tag != "Player" || collision.collider.gameObject.tag != "Enemy")
        {
            if (isVisible)
            {
                AudioManager.instance.PlaySFX(ricochetSound);
            }
        }
    }   
}
