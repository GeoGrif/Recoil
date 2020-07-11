using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScript : AimScript
{
    Vector3 mousePos;
    Vector3 worldMousePos;

    public float timeSpan;

    /**
    * the time in seconds to full charge 
    */
    [SerializeField] private float TotalChargeTime = 0.5f;

    
    public override bool ShouldShoot()
    {
        return Input.GetMouseButtonUp(0);
    }


    public override void Aim()
    {
        mousePos = Input.mousePosition;
        worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        
        if (Input.GetMouseButtonDown(0)) 
        {
            timeSpan = 0;
        }

        if (Input.GetMouseButton(0))
        {
            timeSpan += Time.deltaTime;
        }
        else if (timeSpan > 0)
        {
            Debug.Log(" The current time span of the click is " + timeSpan);
        }
        
       Vector3 handPosition = gameObject.transform.GetChild(0).position;

        Vector2 direction = new Vector2(worldMousePos.x - handPosition.x, worldMousePos.y - transform.position.y);


        //turn to face a direction
        transform.up = direction;
    }

}
