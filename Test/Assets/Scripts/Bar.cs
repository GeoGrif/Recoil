using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    private Image BarImage;

    private void Start()
    {
        BarImage = GetBarImage();

        SetObject();

        BarImage.fillAmount = GetStartingParcentage();
    }

    public abstract Image GetBarImage();
    public abstract void  SetObject();

    public abstract float GetCurrentBarParcentage();

    public abstract float GetStartingParcentage();

    private void Update()
    {
        BarImage.fillAmount = GetCurrentBarParcentage();

       // Debug.Log("the ccurrent fill amount is: " + BarImage.fillAmount);
    }
}
