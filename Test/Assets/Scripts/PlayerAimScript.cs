using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// An implimentation of the AimScript to be used by the player. 
///
public class PlayerAimScript : AimScript
{

    /// <summary>
    /// The position of the mouse in relation of the curernt player/camera. 
    /// </summary>
    private Vector3 mousePos;


    /// <summary>
    /// the current world position of the mouse 
    /// </summary>
    private Vector3 worldMousePos;

    ///<inheritdoc/>
    public override bool ShouldShoot()
    {
        return Input.GetMouseButtonUp(0) && !ShieldScript.shieldIsActive;
    }

    ///<inheritdoc/>
    public override void Aim()
    {
        mousePos = Input.mousePosition;
        worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        
        if (Input.GetMouseButtonDown(0)) 
        {
            _TimeSpan = 0;
        }

        if (Input.GetMouseButton(0))
        {
            _TimeSpan += Time.deltaTime;
        }

        
        Vector3 handPosition = gameObject.transform.GetChild(0).position;

        Vector2 direction = new Vector2(worldMousePos.x - handPosition.x, worldMousePos.y - transform.position.y);


        //turn to face a direction
        transform.up = direction;
    }


    /// <summary>
    /// Get the current charge percantage the character is on
    /// </summary>
    /// <returns> the current charge strength as a value between 0 and 1. </returns>
    public float GetCurrentChagePercentage()
    {
        if (_TimeSpan > 4) _TimeSpan = 4;

        return _TimeSpan / TotalChargeTime;
    }
}
