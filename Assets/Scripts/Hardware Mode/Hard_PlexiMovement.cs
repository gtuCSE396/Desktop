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


    private Hard_BallHeightHandler bhHandler;
    private Hard_BallLocationHandler blHandler;
    private Hard_UIHandler uiHandler;
    private Hard_DrawGraph drawGraph;
    private Transform ballTransform;

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

    private float distanceFactor;
    private float xFactor;
    private float yFactor;

    private float motorAngleFactor = 0.25f; // 90 degree angle rotates plexi by 15 degree.

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

        distanceFactor = 5f / distanceMaxValue;
        xFactor = 3f / xMaxValue;
        yFactor = 3f / yMaxValue;

        drawGraph = GraphObject.GetComponent<Hard_DrawGraph>();
        ballTransform = ballObject.GetComponent<Transform>();

        uiHandler = UIObject.GetComponent<Hard_UIHandler>();
        blHandler = BallLocationHandlerObject.GetComponent<Hard_BallLocationHandler>();
        bhHandler = BallHeightObject.GetComponent<Hard_BallHeightHandler>();
    }

    public void MoveSimulation(float positionX, float positionY, float positionDistance, float motorAngleSouth, float motorAngleNorth, float motorAngleWest, float motorAngleEast)
    {
        float currentXConverted = positionX * xFactor;
        float currentYConverted = positionY * yFactor;
        float currentDistanceConverted = positionDistance * distanceFactor;

        ballTransform.localPosition = new Vector3(currentXConverted, currentDistanceConverted, currentYConverted);

        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate((motorAngleSouth - motorAngleNorth) * motorAngleFactor, 0, (motorAngleEast - motorAngleWest) * motorAngleFactor);

        drawGraph.UpdateGraphs(positionX, positionY, positionDistance);
        uiHandler.DisplayXYDistanceOriginXOriginY(positionX, positionY, positionDistance, originXValue, originYValue);
        blHandler.UpdateBallLocation(positionX, positionY);
        bhHandler.UpdateBallHandler(positionDistance);
    }
}
