using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : Bar
{
    private float MaxHealth;

    private GameObject shield;

    public override Image GetBarImage()
    {
        return GameObject.FindGameObjectWithTag("ShieldBar").GetComponent<Image>();
    }

    public override float GetCurrentBarParcentage()
    {
        return shield.GetComponent<ShieldScript>().shieldHealth / MaxHealth;
    }


    public override float GetStartingParcentage()
    {
        return shield.GetComponent<ShieldScript>().shieldHealth;
    }

    public override void SetObject()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");

        if (shield != null)
        {
            Debug.Log("Shield found ");
        }


        MaxHealth = shield.GetComponent<ShieldScript>().shieldHealth;
    }
}
