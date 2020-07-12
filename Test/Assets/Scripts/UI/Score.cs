using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _ScoreText;

    [SerializeField] private GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string text = _Player.GetComponent<PlayerScript>().score.ToString();

        _ScoreText.text = text;

        Debug.Log(text);
    }
}
