using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimScript : MonoBehaviour
{
    /// <summary>
    ///the position the object this script is attached to is currently at.  
    /// </summary>
    [SerializeField]  private Vector3 ObjectPosition;

    /// <summary>
    ///add our projectile prefab in editor
    /// </summary>
    [SerializeField] public Rigidbody2D ProjectileObject;

    /// <summary>
    /// the initial speed of the projectile when fired. 
    /// </summary>
    [SerializeField]  public float projectileSpeed = 100.0f;


    /// <summary>
    /// the sound made when a projectile is fired.
    /// </summary>
    [SerializeField] private AudioClip shootSound;

    /// <summary>
    ///  the force applied to the object that is firing the projectile. 
    /// </summary>
    public float playerPushSpeed = 300.0f;

    /// <summary>
    /// A time span to hold the current amount of charge.  
    /// </summary>
    public float _TimeSpan;

    /// <summary>
    /// the time in seconds to full charge 
    /// </summary>
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

    /// <summary>
    /// weather the object this script is attached to should shoot or not.  <code>true</code> if the object should shoot.
    /// </summary>
    /// <returns> <code>true</code> if the object should shoot. else <code>false</code>.</returns>
    public abstract bool ShouldShoot();

    /// <summary>
    /// Methed used to set the current aim position of this object 
    /// </summary>
    public abstract void Aim();


    /// <summary>
    /// method called when a projectile is fired.  
    /// </summary>
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
