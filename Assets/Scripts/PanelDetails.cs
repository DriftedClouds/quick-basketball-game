using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDetails : MonoBehaviour
{
    public int pointValue;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If the ball hits the panel, then instruct game manager that there is a multiplier in effect.
    private void OnCollisionEnter(Collision other)
    {
        gameController.SetMultiplier(pointValue);
        Debug.Log("hit panel");
    }
}
