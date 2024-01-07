using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    private Image powerBar;

    private float power = 0.0f;
    private float powerMax = 30.0f;
    private float powerMin = 0.0f;
    private float powerIncrement = 30.0f;
    private bool powerIsIncreasing = true;
    private Vector3 direction = new Vector3(-1.0f, 0.5f, 0.5f);
    private Rigidbody playerRb;
    private GameObject arrow;
    private GameController gameController;

    private float minYForReset = 0.5f;
    private bool ballIsReady = true;
    private Vector3 originalPos;
    private Quaternion originalRot;

    //TODO add baskets, and special panels to bounce off of for extra points. Panels will appear/disappear and vary in value.
    //TODO create automatic shots that we know will go in and bounce off the panels, for testing purposes
    // Start is called before the first frame update
    //TODO create a key that will automatically use the perfect power/angle for shooting off the side, for test purposes
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        playerRb = GetComponent<Rigidbody>();
        arrow = GameObject.Find("Arrow");

        GameObject powerObj = GameObject.Find("PowerBar Fill");
        powerBar = powerObj.GetComponent<Image>();
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballIsReady && gameController.CheckIfGameActive())
        {

            if (Input.GetKey(KeyCode.Space))
            {
                DeterminePower();
                powerBar.fillAmount = power * 3.33f / 100;
            }

            //TODO reset ball after a shot
            //TODO don't allow another shot to happen until ball is reset.
            if (Input.GetKeyUp(KeyCode.Space) && ballIsReady)
            {
                Shoot();
                powerIsIncreasing = true;
                power = 0.0f;
            }
        }
        if (transform.position.y < minYForReset)
        {
            ResetBall();
        }

        //Test function
        if (Input.GetKeyUp(KeyCode.F))
        {
            TrickShot();
        }

    }

    private void DeterminePower()
    {
        if (power > powerMin && power < powerMax && powerIsIncreasing)
        {
            power += powerIncrement * Time.deltaTime;
        }
        else if (power >= powerMax)
        {
            powerIsIncreasing = false;
            power -= powerIncrement * Time.deltaTime;
        }
        else if (power > powerMin && power < powerMax && !powerIsIncreasing)
        {
            power -= powerIncrement * Time.deltaTime;
        }
        else if (power <= powerMin)
        {
            powerIsIncreasing = true;
            power += powerIncrement * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        direction = arrow.GetComponent<ArrowController>().GetArrowDirection();
        playerRb.AddForce(direction * power, ForceMode.Impulse);
        Debug.Log("direction is " + direction);
        Debug.Log("Power is " + power);
        ballIsReady = false;
    }

    private void ResetBall()
    {
        transform.position = originalPos;
        transform.rotation = originalRot;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        ballIsReady = true;
        gameController.ResetMultiplier();
    }

    public void ForceShoot(float customPower)
    {
        power = customPower;
        Shoot();
    }

    public void TrickShot()
    {
        power = 15.9f;
        
        direction = new Vector3(-0.6f, 0.6f, 0.5f);
        playerRb.AddForce(direction * power, ForceMode.Impulse);
    }
}
