using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Vector3 direction = new Vector3(-1.0f, 0.5f, 0.5f);
    //private Vector3 pivotPoint;
    private GameObject pivotTarget;
    private float speed = 40.0f;
    private float maxRotationY = 60.0f;
    private float maxRotationZ = 90.0f;
    private float yAngle;
    private float zAngle;
    //Game Controller is used to see if we are in the middle of an active game
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        pivotTarget = GameObject.Find("Arrow Pivot Target");
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Space) && gameController.CheckIfGameActive())
        {
            AdjustAngle();
        }
    }

    private void AdjustAngle()
    {
        float sideInput = Input.GetAxis("Horizontal");
        yAngle = ConvertAngleToNegative(transform.localRotation.eulerAngles.y);

        if (sideInput > 0 && (yAngle < maxRotationY))
        {
            //direction += new Vector3(0.0f, 0.0f, 0.001f);
            transform.RotateAround(pivotTarget.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        else if (sideInput < 0 && (yAngle > -maxRotationY))
        {
            //direction -= new Vector3(0.0f, 0.0f, 0.001f);
            transform.RotateAround(pivotTarget.transform.position, Vector3.down, speed * Time.deltaTime);
        }

        float verticalInput = Input.GetAxis("Vertical");
        zAngle = ConvertAngleToNegative(transform.localRotation.eulerAngles.z);

        if (verticalInput > 0 && (zAngle < maxRotationZ))
        {
            //direction += new Vector3(0.0f, 0.0f, 0.001f);
            transform.RotateAround(pivotTarget.transform.position, transform.forward, speed * Time.deltaTime);
        }
        else if (verticalInput < 0 && (zAngle > -maxRotationZ))
        {
            //direction -= new Vector3(0.0f, 0.0f, 0.001f);
            transform.RotateAround(pivotTarget.transform.position, -transform.forward, speed * Time.deltaTime);
        }
    }

    //Automatically sets the angle for the arrow controller. Useful for testing.
    public void setAngle(float rotation)
    {
        Debug.Log("rotation is first now " + transform.rotation);
        //transform.rotation = newRotation;
        //transform.RotateAround();
        transform.RotateAround(pivotTarget.transform.position, Vector3.up, rotation);
        Debug.Log("rotation is now " + transform.rotation);
    }

    //Euler angles go from 0 to 360, this converts those angles to go from -180 to 180.
    private float ConvertAngleToNegative(float angle)
    {
        //Converts angle to make sure that it isn't above 360
        angle %= 360;

        float negativeAngle;
        if (angle > 180)
        {
            negativeAngle = angle - 360;
        }
        else
        {
            negativeAngle = angle;
        }
        return negativeAngle;
    }

    public Vector3 GetArrowDirection()
    {
        return -transform.right;
    }
}
