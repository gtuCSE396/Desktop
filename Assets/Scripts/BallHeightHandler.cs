using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHeightHandler : MonoBehaviour
{
    [SerializeField] private GameObject dataHolderObject;
    [SerializeField] private GameObject ballObject;

    private DataHolder dHolder;
    private RectTransform ballTransform;

    private int index = 0;
    private int originZ;

    private float resizingRatio = 0.6f;

    private float originXPosition;
    private float originYPosition;

    private bool stop = false;

    float customTimer;

    void Start()
    {
        dHolder = dataHolderObject.GetComponent<DataHolder>();
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;

        customTimer = Time.fixedTime;
        originZ = 0;
        stop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime >= customTimer && !stop)
        {

           
            customTimer = Time.fixedTime + 0.5f;
        }
    }

    public void UpdateBallHandler(float zPosition)
    {
        float zDistance = (float)(zPosition - originZ) / resizingRatio;
        ballTransform.position = new Vector3(originXPosition, originYPosition + zDistance);     // Apply the calculated distance
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
