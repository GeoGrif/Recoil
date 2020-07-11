using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraScript : MonoBehaviour
{

    
    GameObject player;
    public float height = -10.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Player was null");
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, height);


    }
}

