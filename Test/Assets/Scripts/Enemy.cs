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

    public Rigidbody2D projectile;

    private GameObject player;
    private Vector2 playerPos;
    private float tempFireRate;
    private bool hasFired = false;

    void Start()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Player was null");

        tempFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if(Vector2.Distance(playerPos, transform.position) < aggroRange)
        {
            LookAt(playerPos);

            if (!hasFired)
            {
                FireForwards();
                hasFired = true;
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
    }

    private void LookAt(Vector2 targetPos)
    {    
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        float angleToRotate = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angleToRotate));
    }

    private void FireForwards()
    {
        Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 0));
        projectile.AddForce(transform.right * 100.0f);
    }
}
