using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Enemy))]
public class EnemyAimScript : AimScript
{
    public float fireRate = 1.0f;
    [HideInInspector] public bool canFire = false;
    Enemy enemy;
    
    private GameObject player;
    private Vector2 playerPos;
    private Vector2 targetPos;
    private float tempFireRate = 1.0f;



    void Start()
    {
        enemy = this.GetComponent<Enemy>();
        tempFireRate = fireRate;
    }


    public override bool ShouldShoot()
    {
        return canFire && enemy.lineOfSight;
    }

    void Update()
    {
        playerPos = enemy.playerTarget.transform.position;
        if (!canFire)
        {
            if (Vector2.Distance(playerPos, transform.position) < enemy.range)
            {
                canFire = true;
            }
        }

        //check if we've fired
        if (canFire)
        {
            tempFireRate -= Time.deltaTime;
            if (tempFireRate < 0)
            {
                canFire = false;
                tempFireRate = fireRate;
            }
        }
    }

    public override void Aim()
    {
        targetPos.x = playerPos.x - transform.position.x;
        targetPos.y = playerPos.y - transform.position.y;

        float angleToRotate = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate));
    }

}
