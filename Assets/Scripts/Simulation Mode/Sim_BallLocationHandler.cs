using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim_BallLocationHandler : MonoBehaviour
{

    [SerializeField] private GameObject PlexiObject;
    [SerializeField] private GameObject ballObject;

    private Sim_PlexiMovement pMovement;
    private RectTransform ballTransform;

    private float originX;
    private float originY;

    private float originXPosition;
    private float originYPosition;

    private float resizingRatio = 3f;


    // Start is called before the first frame update
    void Start()
    {
        pMovement = PlexiObject.GetComponent<Sim_PlexiMovement>();
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;

        originX = pMovement.originXValue;      
        originY = pMovement.originYValue;

    }


    public void UpdateBallLocation(float xPosition, float yPosition)
    {
        float xDistance = (xPosition - originX) / resizingRatio;     // Calculate the distance between origin and the ball
        float yDistance = (yPosition - originY) / resizingRatio;

        ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
    }
}
