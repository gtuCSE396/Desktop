using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_BallLocationHandler : MonoBehaviour
{

    [SerializeField] private GameObject PlexiObject;
    [SerializeField] private GameObject ballObject;

    private Hard_PlexiMovement pMovement;
    private RectTransform ballTransform;

    private float originX;
    private float originY;

    private float originXPosition;
    private float originYPosition;

    private float resizingRatioX = 1.5f;
    private float resizingRatioY = 1f;


    // Start is called before the first frame update
    void Awake()
    {
        pMovement = PlexiObject.GetComponent<Hard_PlexiMovement>();
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;
    }

    private void Start()
    {
        originX = pMovement.originXValue;
        originY = pMovement.originYValue;
    }

    public void UpdateBallLocation(float xPosition, float yPosition)
    {
        float xDistance = (xPosition - originX) / resizingRatioX;     // Calculate the distance between origin and the ball
        float yDistance = -(yPosition - originY) / resizingRatioY;

        ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
    }
}
