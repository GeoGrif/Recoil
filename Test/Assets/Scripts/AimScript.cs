using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimScript : MonoBehaviour
{

    /**
    * the position the object this script is aattached to is currently at.  
    */
    [SerializeField]  private Vector3 ObjectPosition;

    //add our projectile prefab in editor
    [SerializeField] public Rigidbody2D ProjectileObject;

    [SerializeField]  public float projectileSpeed = 100.0f;

    [SerializeField] private AudioClip shootSound;


    public float playerPushSpeed = 300.0f;


    public float _TimeSpan;

    /**
    * the time in seconds to full charge 
    */
    [SerializeField] public float TotalChargeTime = 0.5f;



    // Update is called once per frame
    void Update()
    {
        if (GameController.isPaused == false)
        {
            Aim();

            if (ShouldShoot())
            {
                Shoot();
                AudioManager.instance.PlaySFX(shootSound);
            }
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

        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 180);      

        int totalShots = (int)(_TimeSpan / TotalChargeTime) + 1; //calculate the total charge based on 

        for (int i = 0; i < totalShots; i++)
        {
            if (i >= 4) break;

            rot = new Vector3(rot.x, rot.y, rot.z + (90 * i));

            Vector3 handPosition = gameObject.transform.GetChild(i).position;

            //instantiate at the player's position and at player's rotation
            projectile = Instantiate(ProjectileObject, handPosition, Quaternion.Euler(rot));

            Vector3 direction = handPosition - gameObject.transform.position;

            projectile.AddForce(direction * projectileSpeed);

            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * playerPushSpeed);
        }
    }
    
}
