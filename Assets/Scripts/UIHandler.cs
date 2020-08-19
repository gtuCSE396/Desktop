using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject plexiObject;
    [SerializeField] private GameObject xObject;
    [SerializeField] private GameObject yObject;
    [SerializeField] private GameObject distanceObject;

    private Transform ballTransform;
    private Transform plexiTransform;
    private Text positionXText;
    private Text positionYText;
    private Text DistanceText;

    private float currentX;
    private float currentY;
    private float currentDistance;



    // Start is called before the first frame update
    void Start()
    {
        ballTransform = ballObject.GetComponent<Transform>();
        plexiTransform = plexiObject.GetComponent<Transform>();
        positionXText = xObject.GetComponent<Text>();
        positionYText = yObject.GetComponent<Text>();
        DistanceText = distanceObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentX = ballTransform.position.x - plexiTransform.position.x;
        currentY = ballTransform.position.z - plexiTransform.position.z;
        currentDistance = ballTransform.position.y - plexiTransform.position.y;

        positionXText.text = "Position X: " + System.Math.Round(currentX, 2);
        positionYText.text = "Position Y: " + System.Math.Round(currentY, 2);
        DistanceText.text = "Distance: " + System.Math.Round(currentDistance, 2);
    }
}
