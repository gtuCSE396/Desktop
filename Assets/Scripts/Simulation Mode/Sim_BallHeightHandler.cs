using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim_BallHeightHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballObject;

    private RectTransform ballTransform;

    private int originZ;

    private float resizingRatio = 0.6f;

    private float originXPosition;
    private float originYPosition;


    void Start()
    {
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;
        originZ = 0;
    }

    public void UpdateBallHandler(float zPosition)
    {
        float zDistance = (float)(zPosition - originZ) / resizingRatio;
        ballTransform.position = new Vector3(originXPosition, originYPosition + zDistance);     // Apply the calculated distance
    }
}
