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
    [SerializeField] private GameObject ComponentMovementObject;
    [SerializeField] GameObject ClientSideObject;

    private ClientSide clientSide;


    private ComponentMovement cMovement;
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

    private float motorAngleFactor = 0.025f; // 90 degree angle rotates plexi by 15 degree.

    private void Awake()
    {
        xMaxValue = 320f;
        xMinValue = 0f;

        yMaxValue = 240f;
        yMinValue = 0f;

        distanceMaxValue = 100f;
        distanceMinValue = 0f;

        originXValue  = (xMaxValue - xMinValue ) / 2f;
        originYValue = (yMaxValue - yMinValue) / 2f;

        distanceFactor = 6f / distanceMaxValue;
        xFactor = 8f / xMaxValue;
        yFactor = 6f / yMaxValue;

        drawGraph = GraphObject.GetComponent<Hard_DrawGraph>();
        ballTransform = ballObject.GetComponent<Transform>();

        uiHandler = UIObject.GetComponent<Hard_UIHandler>();
        blHandler = BallLocationHandlerObject.GetComponent<Hard_BallLocationHandler>();
        bhHandler = BallHeightObject.GetComponent<Hard_BallHeightHandler>();
        cMovement = ComponentMovementObject.GetComponent<ComponentMovement>();

        clientSide = ClientSideObject.GetComponent<ClientSide>();
    }

    public void MoveSimulation(int positionX, int positionY, int positionDistance, int motorAngleSouth, int motorAngleNorth, int motorAngleWest, int motorAngleEast)
    {
        if (positionX < 0)
            positionX = 0;
        if (positionX > 320)
            positionX = 320;
        if (positionY < 0)
            positionY = 0;
        if (positionY > 240)
            positionY = 240;

        if(positionDistance > 120)
        {
            positionDistance = 120;
        }

        if (positionDistance < 0)
        {
            positionDistance = 0;
        }

        float currentXConverted = positionX * xFactor;
        float currentYConverted = positionY * yFactor;

        float initialX = -4f;
        float initialY = 3f;
        float initialDistance = 0.3f;        

        float bottomX = 8.5f * (positionX - originXValue) / originXValue;
        float bottomY = 6f * (positionY - originYValue) / originYValue;

        float bottomXY = Mathf.Sqrt(Mathf.Pow(bottomX, 2) + Mathf.Pow(bottomY, 2));

        float realDistance = Mathf.Sqrt(Mathf.Pow(positionDistance, 2) - Mathf.Pow(bottomXY, 2));

        realDistance -= 24;

        realDistance *= distanceFactor;

        if(realDistance < 0.5f)
        {
            realDistance = 0.5f;
        }
        Debug.Log("Distance " + realDistance);
        Debug.Log("X " + currentXConverted);
        Debug.Log("Y " + currentYConverted);

        float EastWestRotateValue = motorAngleEast - motorAngleWest;
        float SouthNorthRotateValue = motorAngleSouth - motorAngleNorth;

        if(EastWestRotateValue > 600)
        {
            EastWestRotateValue = 600;
        }
        if (EastWestRotateValue < -600)
        {
            EastWestRotateValue = -600;
        }
        if (SouthNorthRotateValue > 600)
        {
            SouthNorthRotateValue = 600;
        }
        if (SouthNorthRotateValue < -600)
        {
            SouthNorthRotateValue = -600;
        }

        Debug.Log("South North " + SouthNorthRotateValue);
        Debug.Log("East West " + EastWestRotateValue);

        if (float.IsNaN(realDistance))
            realDistance = 0;

        ballTransform.localPosition = new Vector3(initialX + currentXConverted, initialDistance + realDistance , initialY - currentYConverted);        
        ballTransform.localPosition = new Vector3(initialX + currentXConverted, initialDistance + realDistance , initialY - currentYConverted);
        cMovement.MoveComponents(SouthNorthRotateValue / 10, EastWestRotateValue / 10);

        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate((SouthNorthRotateValue) * motorAngleFactor, 0, (EastWestRotateValue) * motorAngleFactor);

        drawGraph.UpdateGraphs(positionX, positionY, positionDistance);
        uiHandler.DisplayXYDistanceOriginXOriginY(positionX, positionY, positionDistance, originXValue, originYValue);
        blHandler.UpdateBallLocation(positionX, positionY);
        bhHandler.UpdateBallHandler(positionDistance);

        clientSide.SendWithParameter("M " + positionX + " " + positionY + " " + positionDistance);
    }
}
