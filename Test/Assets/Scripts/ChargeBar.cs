using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : Bar
{
    private GameObject Player;

    private float MaxCharge;

    public override Image GetBarImage()
    {
        return gameObject.GetComponent<Image>();
    }

    public override float GetCurrentBarParcentage()
    {
        return Player.GetComponent<PlayerAimScript>().GetCurrentChagePercentage();
    }

    public override float GetStartingParcentage()
    {
       return Player.GetComponent<PlayerAimScript>().GetCurrentChagePercentage();
    }

    public override void SetObject()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            Debug.Log("Player found ");
        }
    }

}
