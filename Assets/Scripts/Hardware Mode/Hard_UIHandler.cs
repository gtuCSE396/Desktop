using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hard_UIHandler : MonoBehaviour
{

    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject plexiObject;
    [SerializeField] private GameObject xObject;
    [SerializeField] private GameObject yObject;
    [SerializeField] private GameObject distanceObject;
    [SerializeField] private GameObject originXObject;
    [SerializeField] private GameObject originYObject;

    private Transform ballTransform;
    private Transform plexiTransform;
    private Text positionXText;
    private Text positionYText;
    private Text DistanceText;

    private Text originXText;
    private Text originYText;

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
        originXText = originXObject.GetComponent<Text>();
        originYText = originYObject.GetComponent<Text>();
        DistanceText = distanceObject.GetComponent<Text>();
    }
    public void DisplayXYDistanceOriginXOriginY(float X, float Y, float Distance, float originX, float originY)
    {
        positionXText.text = "Position X: " + X;
        positionYText.text = "Position Y: " + Y;
        DistanceText.text = "Distance: " + Mathf.Round(Distance * 100f) / 100f;
        originXText.text = "Origin X: " + originX;
        originYText.text = "Origin Y: " + originY;
    }
}
