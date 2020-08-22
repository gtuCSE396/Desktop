using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_PlexiMovement : MonoBehaviour
{
    [SerializeField] private GameObject BallHeightObject;
    [SerializeField] private GameObject BallLocationHandlerObject;
    [SerializeField] private GameObject UIObject;
    [SerializeField] private GameObject GraphObject;
    [SerializeField] private GameObject ballObject;
    private Transform tForm;
    private Rigidbody rBody;
    private bool moveUp;
    private float vibrationTimer;
    private float rotateTimer;

    private Hard_BallHeightHandler bhHandler;
    private Hard_BallLocationHandler blHandler;
    private Hard_UIHandler uiHandler;
    private Hard_DrawGraph drawGraph;
    private Transform ballTransform;
    private Rigidbody ballRigid;

    public float xMaxValue;
    public float xMinValue;

    public float yMaxValue;
    public float yMinValue;

    public float distanceMaxValue;
    public float distanceMinValue;

    public float originXValue;
    public float originYValue;

    public float rotatePositionValue = 0.003f;
    public float rotateVelocityValue = 0.01f;

    private float reversedistanceFactor = 100f / 5f;
    private float reversexFactor = 500f / 3f;
    private float reverseyFactor = 500f / 3f;

    private float currentXConverted;
    private float currentYConverted;
    private float currentZConverted;

    private void Awake()
    {
        xMaxValue = 500f;
        xMinValue = 0f;

        yMaxValue = 500f;
        yMinValue = 0f;

        distanceMaxValue = 100f;
        distanceMinValue = 0f;

        originXValue  = (xMaxValue - xMinValue ) / 2f;
        originYValue = (yMaxValue - yMinValue) / 2f;

        reversedistanceFactor = distanceMaxValue / 5f;
        reversexFactor = xMaxValue / 3f;
        reverseyFactor = yMaxValue / 3f;

        drawGraph = GraphObject.GetComponent<Hard_DrawGraph>();
        ballTransform = ballObject.GetComponent<Transform>();
        ballRigid = ballObject.GetComponent<Rigidbody>();
        vibrationTimer = Time.fixedTime;
        rotateTimer = Time.fixedTime;
        moveUp = false;
        tForm = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
        uiHandler = UIObject.GetComponent<Hard_UIHandler>();
        blHandler = BallLocationHandlerObject.GetComponent<Hard_BallLocationHandler>();
        bhHandler = BallHeightObject.GetComponent<Hard_BallHeightHandler>();
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime >= vibrationTimer)
        {
            // Switch direction
            if (!moveUp)
            {
                moveUp = true;
            }
            else
            {
                moveUp = false;
            }

            // Do the vibration
            if (moveUp)
            {
                rBody.AddForce(new Vector3(0, 5f), ForceMode.VelocityChange);
            }
            else
            {
                rBody.AddForce(rBody.velocity * -2, ForceMode.VelocityChange);
            }

            vibrationTimer = Time.fixedTime + 0.05f;
        }
        if (Time.fixedTime >= rotateTimer)
        {

            // Balance the ball by velocity

            if (ballRigid.velocity.x > 0)
            {
                transform.Rotate(0, 0, rotateVelocityValue);
            }
            if (ballRigid.velocity.x < 0)
            {
                transform.Rotate(0, 0, -rotateVelocityValue);
            }

            if (ballRigid.velocity.z > 0)
            {
                transform.Rotate(-rotateVelocityValue, 0, 0);
            }

            if (ballRigid.velocity.z < 0)
            {
                transform.Rotate(rotateVelocityValue, 0, 0);
            }

            // Balance the ball by position

            if (ballTransform.position.x > transform.position.x)
            {
                transform.Rotate(0, 0, rotatePositionValue);
            }

            if (ballTransform.position.x < transform.position.x)
            {
                transform.Rotate(0, 0, -rotatePositionValue);
            }

            if (ballTransform.position.z > transform.position.z)
            {
                transform.Rotate(-rotatePositionValue, 0, 0);
            }

            if (ballTransform.position.z < transform.position.z)
            {
                transform.Rotate(rotatePositionValue, 0, 0);
            }

            rotateTimer = Time.fixedTime + 0.02f;
        }
    }
    void Update()
    {
        // X -> X, Y -> Distance, Z -> Y

        // X value
        currentXConverted = originXValue + ballTransform.localPosition.x * reversexFactor;
        // Distance value
        currentYConverted = (ballTransform.localPosition.y - transform.localPosition.y) * reversedistanceFactor;
        //Y value
        currentZConverted = originYValue + ballTransform.localPosition.z * reverseyFactor;

        drawGraph.UpdateGraphs(currentXConverted, currentZConverted, currentYConverted);
        uiHandler.DisplayXYDistanceOriginXOriginY(currentXConverted, currentZConverted, currentYConverted, originXValue, originYValue);
        blHandler.UpdateBallLocation(currentXConverted, currentZConverted);
        bhHandler.UpdateBallHandler(currentYConverted);
    }
}
