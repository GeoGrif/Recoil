using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{

    private GameObject Player;

    private float MaxHealth;

    public override Image GetBarImage()
    {
        return gameObject.GetComponent<Image>();
    }

    public override float GetCurrentBarParcentage()
    {
        return Player.GetComponent<PlayerScript>().health / MaxHealth;
    }


    public override float GetStartingParcentage()
    {
        return Player.GetComponent<PlayerScript>().health;
    }

    public override void SetObject()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null) 
        {
            Debug.Log("Player found ");
        }


        MaxHealth = Player.GetComponent<PlayerScript>().health;
    }
}
