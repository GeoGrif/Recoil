﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimScript : MonoBehaviour
{

    /**
    * The current world position this is aiming at.  
    */
    [SerializeField]  private Vector3 AimPosition;

    /**
    * the position the object this script is aattached to is currently at.  
    */
    [SerializeField]  private Vector3 ObjectPosition;

    //add our projectile prefab in editor
    [SerializeField] public Rigidbody2D ProjectileObject;


    public float projectileSpeed = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    /**
    * weather the object this script is attached to should shoot or not.  
    */
    public abstract bool ShouldShoot();

    /**
    * Methed used to set current aim position of this object 
    */
    public abstract void Aim();
 

    /**
    * method called when a projectile is fired.  
    */
    private void Shoot()
    {
        Rigidbody2D projectile;

        //instantiate at the player's position and at player's rotation
        projectile = Instantiate(ProjectileObject, transform.position, transform.rotation);
        Debug.Log("Shot fired!");


        //add the force in the correct direction
        projectile.AddForce(transform.up * projectileSpeed);
    }

}
