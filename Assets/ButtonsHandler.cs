﻿using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    [SerializeField] private GameObject buttonBounceObject;
    [SerializeField] private GameObject buttonBalanceObject;
    [SerializeField] private GameObject buttonSquareObject;
    [SerializeField] private GameObject buttonTriangleObject;

    [SerializeField] private GameObject deactivatedObject;
    [SerializeField] private GameObject activatedObject;
    [SerializeField] private GameObject squareRedObject;
    [SerializeField] private GameObject squareGreenObject;
    [SerializeField] private GameObject triangleRedObject;
    [SerializeField] private GameObject triangleGreenObject;

    [SerializeField] private GameObject plexiObject;
    [SerializeField] private GameObject InputXObject;
    [SerializeField] private GameObject InputYObject;

    private GameObject cloneBounceDeactivated;
    private GameObject cloneBounceActivated;

    private GameObject cloneBalanceDeactivated;
    private GameObject cloneBalanceActivated;

    private GameObject cloneSquareDeactivated;
    private GameObject cloneSquareActivated;

    private GameObject cloneTriangleDeactivated;
    private GameObject cloneTriangleActivated;

    private bool bounceActivated;
    private bool balanceActivated;
    private bool squareActivated;
    private bool triangleActivated;

    private Sim_PlexiMovement simMovement;

    // Start is called before the first frame update
    void Start()
    {
        cloneBalanceActivated = Instantiate(activatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        cloneBounceDeactivated = Instantiate(deactivatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        cloneSquareDeactivated = Instantiate(squareRedObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        cloneTriangleDeactivated = Instantiate(triangleRedObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        
        bounceActivated = false;
        balanceActivated = true;
        squareActivated = false;
        triangleActivated = false;

        simMovement = plexiObject.GetComponent<Sim_PlexiMovement>();
    }

    public void balanceSwitchActivated()
    {
        Destroy(cloneBalanceDeactivated);
        cloneBalanceActivated = Instantiate(activatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        balanceActivated = true;
        simMovement.balanceLocationX = float.Parse(InputXObject.GetComponent<Text>().text);
        simMovement.balanceLocationY = float.Parse(InputYObject.GetComponent<Text>().text);
        simMovement.balancing = true;
    }

    public void balanceSwitchDeactivated()
    {
        Destroy(cloneBalanceActivated);
        cloneBalanceDeactivated = Instantiate(deactivatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        balanceActivated = false;
        simMovement.balanceLocationX = simMovement.originXValue;
        simMovement.balanceLocationY = simMovement.originYValue;
        simMovement.balancing = false;
    }

    public void bounceSwitchActivated()
    {
        Destroy(cloneBounceDeactivated);
        cloneBounceActivated = Instantiate(activatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        bounceActivated = true;
        simMovement.bouncing = true;
    }

    public void bounceSwitchDeactivated()
    {
        Destroy(cloneBounceActivated);
        cloneBounceDeactivated = Instantiate(deactivatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        bounceActivated = false;
        simMovement.bouncing = false;
    }

    public void squareSwitchActivated()
    {
        bounceSwitchDeactivated();
        Destroy(cloneSquareDeactivated);
        cloneSquareActivated = Instantiate(squareGreenObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        squareActivated = true;
        simMovement.drawSquare();
    }

    public void squareSwitchDeactivated()
    {
        Destroy(cloneSquareActivated);
        cloneSquareDeactivated = Instantiate(squareRedObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        squareActivated = false;
    }

    public void triangleSwitchActivated()
    {
        bounceSwitchDeactivated();
        Destroy(cloneTriangleDeactivated);
        cloneTriangleActivated = Instantiate(triangleGreenObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        triangleActivated = true;
        simMovement.drawTriangle();
    }

    public void triangleSwitchDeactivated()
    {
        Destroy(cloneTriangleActivated);
        cloneTriangleDeactivated = Instantiate(triangleRedObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        triangleActivated = false;
    }

    public void handleBalance()
    {
        if (balanceActivated)
        {
            balanceSwitchDeactivated();
        }
        else
        {
            balanceSwitchActivated();
        }
    }

    public void handleBounce()
    {
        if (bounceActivated)
        {
            bounceSwitchDeactivated();
        }
        else
        {
            bounceSwitchActivated();
        }
    }

    public void handleSquare()
    {
        if (squareActivated)
        {
            squareSwitchDeactivated();
        }
        else
        {
            squareSwitchActivated();
        }
    }

    public void handleTriangle()
    {
        if (triangleActivated)
        {
            triangleSwitchDeactivated();
        }
        else
        {
             triangleSwitchActivated();
        }
    }
}
