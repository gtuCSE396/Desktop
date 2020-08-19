﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLocationHandler : MonoBehaviour
{

    [SerializeField] private GameObject dataHolderObject;
    [SerializeField] private GameObject ballObject;

    private DataHolder dHolder;
    private RectTransform ballTransform;

    private int originX;
    private int originY;

    private float originXPosition;
    private float originYPosition;

    private int index = 0;

    private int resizingRatio = 3;

    private bool stop = false;

    float customTimer;

    // Start is called before the first frame update
    void Start()
    {
        dHolder = dataHolderObject.GetComponent<DataHolder>();
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;

        originX = (dHolder.xMaxValue - dHolder.xMinValue) / 2;      // Calculate origin of the given data
        originY = (dHolder.yMaxValue - dHolder.yMinValue) / 2;

        customTimer = Time.fixedTime;
        stop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime >= customTimer && !stop) 
        {
            int xDistance = (dHolder.xValues[index] - originX) / resizingRatio;     // Calculate the distance between origin and the ball
            int yDistance = (dHolder.yValues[index] - originY) / resizingRatio;

            ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
            index++;
            if (index == dHolder.listMaxElements)       // If data list ends, start from the beginning
                index = 0;
            customTimer = Time.fixedTime + 0.5f;
        }
    }
    public void Stop()
    {
        stop = true;
        index = 0;
        ballTransform.position = new Vector3(originXPosition, originYPosition);
    }

    public void StartAgain()
    {
        customTimer = Time.fixedTime;
        stop = false;
    }
}
