using System.Collections;
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


    public void UpdateBallLocation(float xPosition, float yPosition)
    {
        float xDistance = (xPosition - originX) / resizingRatio;     // Calculate the distance between origin and the ball
        float yDistance = (yPosition - originY) / resizingRatio;

        ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
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
