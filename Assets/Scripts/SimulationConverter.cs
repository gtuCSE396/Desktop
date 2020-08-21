using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationConverter : MonoBehaviour
{

    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject DataHolderObject;
    [SerializeField] private GameObject UIHandlerObject;

    private Transform tForm;
    private Rigidbody rBody;
    private bool moveUp;
    private bool moveDown;

    private bool stop = false;
    private int index = 0;

    private Transform ballTransform;
    private Rigidbody ballRigid;
    private DataHolder dHolder;
    private UIHandler uihandler;

    private float originZPosition;
    private float originYPosition;
    private float originXPosition;

    float customTimer;

    // Embedded Distance 100 is 5 in 3D Simulation
    // Embedded X max 500 is 3 in 3D Simulation
    // Embedded X min 0 is -3 in 3D Simulation
    // Embedded Y max 500 is 3 in 3D Simulation
    // Embedded Y min 0 is -3 in 3D Simulation

    // Origin Z is 69 in 3D Simulation
    // Origin Y is -497.049 in 3D Simulation
    // Origin X is -897.8567 in 3D Simulation

    private float distanceFactor = 5f / 100f;
    private float xFactor = 3f / 500f;
    private float yFactor = 3f / 500f;
    private float motorAngleFactor = 0.25f; // 90 degree angle rotates plexi by 15 degree.

    private void Awake()
    {
        uihandler = UIHandlerObject.GetComponent<UIHandler>();
        dHolder = DataHolderObject.GetComponent<DataHolder>();
        ballTransform = ballObject.GetComponent<Transform>();
        ballRigid = ballObject.GetComponent<Rigidbody>();
        customTimer = Time.fixedTime;
        moveDown = true;
        moveUp = false;
        tForm = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;
        originYPosition = ballTransform.position.z;

        stop = true;
    }

    void Update()
    {
        if (Time.fixedTime >= customTimer && !stop)
        {
            ballTransform.position = new Vector3(dHolder.xValues[index]*xFactor, dHolder.yValues[index]*yFactor, dHolder.zValues[index]*distanceFactor);

            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.Rotate(dHolder.motorSouthAngles[index] - dHolder.motorNorthAngles[index], 0, dHolder.motorEastAngles[index] - dHolder.motorWestAngles[index]);

            uihandler.DisplayXYDistanceOriginXOriginY(dHolder.xValues[index], dHolder.yValues[index], dHolder.zValues[index], (dHolder.xMaxValue - dHolder.xMinValue)/2, (dHolder.yMaxValue - dHolder.yMinValue) / 2);

            index++;
            if (index == dHolder.listMaxElements)       // If data list ends, start from the beginning
                index = 0;

            customTimer = Time.fixedTime + 0.1f;
        }
    }
    public void Stop()
    {
        stop = true;
        index = 0;
        ballTransform.position = new Vector3(originXPosition, originYPosition, originZPosition);
    }

    public void StartAgain()
    {
        customTimer = Time.fixedTime;
        stop = false;
    }
}
