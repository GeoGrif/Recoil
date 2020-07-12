using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraScript : MonoBehaviour
{


    private static float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1f;

    GameObject player;
    public float height = -10.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Player was null");

        //initialPosition = transform.localPosition;

    }



    // Update is called once per frame
    void Update()
    {

        if (shakeDuration > 0 && !GameController.isPaused)
        {
            Vector3 shakePoint = Random.insideUnitCircle * shakeMagnitude;

            shakePoint.z = gameObject.transform.position.z;

            transform.position = player.GetComponent<Transform>().position + shakePoint;

            shakeDuration -= Time.deltaTime * dampingSpeed;

        }
        else if(shakeDuration > 0)
        {
            shakeDuration = 0f;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, height);
        }

    }

    public static void TriggerShake()
    {
        shakeDuration = .5f;
        Debug.Log("TriggerShake");
    }
}

