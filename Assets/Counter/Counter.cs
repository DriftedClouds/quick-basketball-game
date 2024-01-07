using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int Count = 0;
    private GameController gameController;

    private void Start()
    {
        Count = 0;
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.ScoreBasket();
    }
}
