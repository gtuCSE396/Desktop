using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Sim_PlexiMovement : MonoBehaviour
{
    [SerializeField] private GameObject BallHeightObject;
    [SerializeField] private GameObject BallLocationHandlerObject;
    [SerializeField] private GameObject UIObject;
    [SerializeField] private GameObject GraphObject;
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject floorObject;
    [SerializeField] private GameObject clientObject;


    [SerializeField] private GameObject ButtonsObject;
    private Transform tForm;
    private Rigidbody rBody;
    private bool bounced;
    private bool bouncedBackup;
    private float vibrationTimer;
    private float rotateTimer;

    private Sim_BallHeightHandler bhHandler;
    private Sim_BallLocationHandler blHandler;
    private Sim_UIHandler uiHandler;
    private Sim_DrawGraph drawGraph;
    private Transform ballTransform;
    private Rigidbody ballRigid;

    private ButtonsHandler bHandler;

    public float xMaxValue;
    public float xMinValue;

    public float yMaxValue;
    public float yMinValue;

    public float distanceMaxValue;
    public float distanceMinValue;

    public float originXValue;
    public float originYValue;

    public float rotatePositionValue = 0.1f;
    public float rotateVelocityValue = 5f;

    private float reversedistanceFactor = 100f / 5f;
    private float reversexFactor = 500f / 3f;
    private float reverseyFactor = 500f / 3f;

    private float currentXConverted;
    private float currentYConverted;
    private float currentZConverted;

    public float balanceLocationX;
    public float balanceLocationY;

    float errorX;
    float errorZ;

    float balancingPowerX;
    float balancingPowerZ;

    public int balanceMode = 1;


    private bool ready;

    public bool bouncing;
    public bool balancing;

    private void Awake()
    {
        xMaxValue = 500f;
        xMinValue = 0f;

        yMaxValue = 500f;
        yMinValue = 0f;

        distanceMaxValue = 100f;
        distanceMinValue = 0f;

        originXValue  = (xMaxValue - xMinValue ) / 2f;
        originYValue = (yMaxValue - yMinValue) / 2f;

        reversedistanceFactor = distanceMaxValue / 5f;
        reversexFactor = xMaxValue / 3f;
        reverseyFactor = yMaxValue / 3f;

        drawGraph = GraphObject.GetComponent<Sim_DrawGraph>();
        ballTransform = ballObject.GetComponent<Transform>();
        ballRigid = ballObject.GetComponent<Rigidbody>();
        vibrationTimer = Time.fixedTime;
        rotateTimer = Time.fixedTime;
        bounced = false;
        tForm = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
        uiHandler = UIObject.GetComponent<Sim_UIHandler>();
        blHandler = BallLocationHandlerObject.GetComponent<Sim_BallLocationHandler>();
        bhHandler = BallHeightObject.GetComponent<Sim_BallHeightHandler>();

        bHandler = ButtonsObject.GetComponent<ButtonsHandler>();

        ready = true;
        balancing = true;
        bouncing = false;

        tForm.position = new Vector3(0, 0, 0);
        rBody.isKinematic = true;

        balanceLocationX = originXValue;
        balanceLocationY = originYValue;
    }

    void Update()
    {
        Debug.Log("ErrorX = " + Mathf.Abs(errorX));
        Debug.Log("ErrorZ = " + Mathf.Abs(errorZ));
        Debug.Log("ErrorBouncing = " + bouncing);
        Debug.Log("ErrorBounbounced = " + bounced);

        if (bouncing && !bounced && ballTransform.position.y - transform.position.y <= 1f && Mathf.Abs(errorX) < 10f && Mathf.Abs(errorZ) < 10f)
        {
            bounced = true;
            StartCoroutine(BounceBall());
        }

        if(balancing == true)
        {
            errorX = (transform.position.x - ballTransform.position.x) * reversexFactor - (originXValue - balanceLocationX);
            errorZ = (transform.position.z - ballTransform.position.z) * reverseyFactor - (originYValue - balanceLocationY);

            balancingPowerX = ballRigid.velocity.x * rotateVelocityValue;
            balancingPowerZ = ballRigid.velocity.z * rotateVelocityValue;

            BalanceTheBall();
            
        }
        else
        {
            errorX = (transform.position.x - ballTransform.position.x) * reversexFactor - (originXValue - originXValue);
            errorZ = (transform.position.z - ballTransform.position.z) * reverseyFactor - (originYValue - originYValue);

            balancingPowerX = ballRigid.velocity.x * rotateVelocityValue;
            balancingPowerZ = ballRigid.velocity.z * rotateVelocityValue;

            BalanceTheBall();
        }

        // X -> X, Y -> Distance, Z -> Y

        // X value
        currentXConverted = originXValue + ballTransform.localPosition.x * reversexFactor;
        // Distance value
        currentYConverted = (ballTransform.localPosition.y - transform.localPosition.y) * reversedistanceFactor - 14.2158f;
        currentYConverted = (float)Mathf.Round(currentYConverted);
        //currentYConverted -= 14.2158f; // dirty fix
       //Y value
       currentZConverted = originYValue + ballTransform.localPosition.z * reverseyFactor;

        string data =  Mathf.Round(currentXConverted).ToString() + " " + Mathf.Round(currentZConverted).ToString() + " " + Mathf.Round(currentYConverted).ToString();

        clientObject.GetComponent<ClientSide>().SendWithParameter(data);

        drawGraph.UpdateGraphs(currentXConverted, currentZConverted, currentYConverted);
        uiHandler.DisplayXYDistanceOriginXOriginY(currentXConverted, currentZConverted, currentYConverted, originXValue, originYValue);
        blHandler.UpdateBallLocation(currentXConverted, currentZConverted);
        bhHandler.UpdateBallHandler(currentYConverted);

    }
    public void drawSquare()
    {
        StartCoroutine(squareCoroutine());
    }

    public void drawTriangle()
    {
        StartCoroutine(triangleCoroutine());
    }

    IEnumerator squareCoroutine()
    {
        bouncing = false;
        balancing = true;

        yield return new WaitForSeconds(0.4f); // Wait for drawing

        balanceLocationX = originXValue;
        balanceLocationY = originYValue;

        yield return new WaitForSeconds(4);

        balanceLocationX = 150;
        balanceLocationY = 400;

        yield return new WaitForSeconds(4);

        balanceLocationX = 400;
        balanceLocationY = 400;

        yield return new WaitForSeconds(4);

        balanceLocationX = 400;
        balanceLocationY = 150;

        yield return new WaitForSeconds(4);

        balanceLocationX = 150;
        balanceLocationY = 150;

        yield return new WaitForSeconds(4);

        balanceLocationX = 150;
        balanceLocationY = 400;

        yield return new WaitForSeconds(4);

        balanceLocationX = originXValue;
        balanceLocationY = originYValue;
        bHandler.squareSwitchDeactivated();
        bHandler.ActivateAll();
    }
    IEnumerator triangleCoroutine()
    {
        bouncing = false;
        balancing = true;

        balanceLocationX = originXValue;
        balanceLocationY = originYValue;

        yield return new WaitForSeconds(4);

        balanceLocationX = 250;
        balanceLocationY = 400;

        yield return new WaitForSeconds(4);

        balanceLocationX = 400;
        balanceLocationY = 150;

        yield return new WaitForSeconds(4);

        balanceLocationX = 150;
        balanceLocationY = 150;

        yield return new WaitForSeconds(4);

        balanceLocationX = 250;
        balanceLocationY = 400;

        yield return new WaitForSeconds(4);

        balanceLocationX = originXValue;
        balanceLocationY = originYValue;
        bHandler.triangleSwitchDeactivated();
        bHandler.ActivateAll();
    }
    public void balanceSoft()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler((errorZ * rotatePositionValue) - balancingPowerZ, 0, (-errorX * rotatePositionValue) + balancingPowerX), Time.deltaTime * 10f);
    }
    public void balanceHard()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(new Vector3((errorZ * rotatePositionValue) - balancingPowerZ, 0, (-errorX * rotatePositionValue) + balancingPowerX));
    }

    public void BalanceTheBall()
    {
        if(balanceMode == 1)
        {
            balanceHard();
        }
        else if(balanceMode == 2)
        {
            balanceSoft();
        }
    }

    IEnumerator BounceBall()
    {
        rBody.isKinematic = false;
        Debug.Log("Assigned jumped though" + bounced);
        rBody.AddForce(new Vector3(0, 2f), ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.2f);
        rBody.velocity = Vector3.zero;
        rBody.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        rBody.isKinematic = true;
        Debug.Log("Assigned false though" + bounced);
        bounced = false;
    }

}
