using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScript : AimScript
{
    Vector3 mousePos;
    Vector3 worldMousePos;
    

    
    public override bool ShouldShoot()
    {
        return Input.GetMouseButtonDown(0);
    }


    public override void Aim()
    {
        mousePos = Input.mousePosition;
        worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

       Vector3 handPosition = gameObject.transform.GetChild(0).position;

        Vector2 direction = new Vector2(worldMousePos.x - handPosition.x, worldMousePos.y - transform.position.y);


        //turn to face a direction
        transform.up = direction;

    }

}
