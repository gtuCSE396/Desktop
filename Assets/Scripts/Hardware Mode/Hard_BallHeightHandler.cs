using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_BallHeightHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballObject;

    private RectTransform ballTransform;

    private float originZ;

    private float resizingRatio = 1f;

    private float originXPosition;
    private float originYPosition;


    void Start()
    {
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.localPosition.x;     // Save initial position of the ball
        originYPosition = ballTransform.localPosition.y;
        originZ = 0f;
    }

    public void UpdateBallHandler(float zPosition)
    {
        float zDistance =(zPosition - originZ) / resizingRatio;
        ballTransform.localPosition = new Vector3(originXPosition, originYPosition + zDistance);     // Apply the calculated distance
    }
}
