using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    public float fireRate = 1.0f;
    public int health = 10;
    public float moveSpeed = 3.0f;
    public float aggroRange = 5.0f;
    public float range = 5.0f;
    public float projectileSpeed = 100.0f;


    public Rigidbody2D projectile;

    private GameObject player;
    private BoxCollider2D playerCollider;
    private BoxCollider2D shieldCollider;
    private Vector3 playerPos;
    private float tempFireRate;
    private bool hasFired = false;
    private bool lineOfSight = false;

    void Start()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Player was null");
        playerCollider = player.GetComponent<BoxCollider2D>();
        shieldCollider = player.GetComponentInChildren<BoxCollider2D>();
        tempFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if(Vector2.Distance(playerPos, transform.position) < aggroRange && lineOfSight)
        {
            LookAt(playerPos);

            if (!hasFired)
            {
                if(Vector2.Distance(playerPos, transform.position) < range)
                {
                    FireForwards();
                    hasFired = true;
                }
            }
        }

        //check if we've fired
        if(hasFired)
        {
            tempFireRate -= Time.deltaTime;
            if(tempFireRate < 0)
            {
                hasFired = false;
                tempFireRate = fireRate;
            }
        }

        Vector3 dir = playerPos - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        Debug.DrawRay(transform.position, dir, Color.green, 0.0f);

        if (hit.collider != playerCollider)
        {
            //move around
            lineOfSight = false;
        }
        else
        {
            lineOfSight = true;
        }                       
    }

    private void LookAt(Vector2 targetPos)
    {    
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        float angleToRotate = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate));
    }

    private void FireForwards()
    {
        Rigidbody2D proj;
        proj = Instantiate(projectile, transform.position, transform.rotation);
        proj.AddForce(transform.right * projectileSpeed);
    }
}
