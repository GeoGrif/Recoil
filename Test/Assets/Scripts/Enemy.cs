﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    //public float moveSpeed = 3.0f;
    //public float aggroRange = 5.0f;
    public float range = 5.0f;

    public bool lineOfSight = false;

    public GameObject playerTarget;
    public BoxCollider2D targetCollider;
    public BoxCollider2D targetShieldCollider;
    public Vector3 playerPos;
    private float tempFireRate;
    private Vector3 dir;

    void Start()
    {
        playerTarget = GameObject.Find("Player");
        Assert.IsNotNull(playerTarget, "Player was null");
        targetCollider = playerTarget.GetComponent<BoxCollider2D>();
        targetShieldCollider = GameObject.Find("Shield").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerTarget.transform.position;

        dir = playerPos - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        Debug.DrawRay(transform.position, dir, Color.green, 0.0f);

        if (hit.collider != targetCollider && hit.collider != targetShieldCollider)
        {
            lineOfSight = false;
        }
        else
        {
            lineOfSight = true;
        }                       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Projectile>() != null)
        {
            Destroy(other.gameObject);
            health--;
        }
    }
}
