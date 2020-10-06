using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScript : AimScript
{
    Vector3 mousePos;
    Vector3 worldMousePos;

    
    public override bool ShouldShoot()
    {
        return Input.GetMouseButtonUp(0) && !ShieldScript.shieldIsActive;
    }


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



    public float GetCurrentChagePercentage()
    {
        if (_TimeSpan > 4) _TimeSpan = 4;

        return _TimeSpan / TotalChargeTime;
    }
}
