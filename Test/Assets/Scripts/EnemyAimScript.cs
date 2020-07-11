using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Enemy))]
public class EnemyAimScript : AimScript
{
    public float fireRate = 1.0f;
    [HideInInspector] public bool canFire = false;
    private bool justFired = false;
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
        return canFire && enemy.lineOfSight && !justFired;
    }

    public override void Aim()
    {
        targetPos.x = enemy.playerPos.x - transform.position.x;
        targetPos.y = enemy.playerPos.y - transform.position.y;

        float angleToRotate = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate - 90));

        if (Vector2.Distance(enemy.playerPos, transform.position) < enemy.range)
        {
            canFire = true;
            justFired = true;
        }
        else
        {
            canFire = false;
        }

        //check if we've fired
        if (justFired)
        {
            tempFireRate -= Time.deltaTime;
            if (tempFireRate < 0)
            {
                justFired = false;
                tempFireRate = fireRate;
            }
        }
    }

}
