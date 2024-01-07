using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
//using UnityEngine.InputSystem;

public class FindOptimalPath : MonoBehaviour
{
    private ArrowController arrowController;
    private BallController ballController;

    //TODO get rid of the bad code in arrowcontroller/ballcontroller that we use for testing, and replace it with a new eventsystem
    //With new eventsystem you can simulate user input for tests

    // Start is called before the first frame update
    void Start()
    {
        arrowController = GameObject.Find("Arrow").GetComponent<ArrowController>();
        ballController = GameObject.Find("Basketball").GetComponent<BallController>();
        //for i = 1-100 or something, adjust angle/power

        StartCoroutine(TestAngles());

    }

    public void RunTest ()
    {
        //Input.GetAxisRaw("Horizontal").Returns(1);
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator TestAngles()
    {
        Quaternion testAngle = new Quaternion(1, 1, 1, 1);



        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(1f);

            testAngle = new Quaternion(i, 1, 1, 1);
            float rotation = i * 2;
            if (i == 0)
            {
                arrowController.setAngle(20.0f);
            }
            arrowController.setAngle(1.0f);
            ballController.ForceShoot(20.0f);
            yield return new WaitForSeconds(2f);
            //Plan is for it to fire a ball and wait for the ball to come back
            //direciton of Vector3(-0.6f, 0.6f, 0.5f) with shoot power of 16 seems to work from testing.
        }
    }
}
