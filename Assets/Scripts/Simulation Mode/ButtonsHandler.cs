using System.Collections;
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

    [SerializeField] private GameObject Text1Object;
    [SerializeField] private GameObject Text2Object;

    [SerializeField] private GameObject Error1Object;
    [SerializeField] private GameObject Error2Object;

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
        StartCoroutine(BalanceSlowly());
        Destroy(cloneBalanceDeactivated);
        cloneBalanceActivated = Instantiate(activatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        balanceActivated = true;
        float tempX = float.Parse(InputXObject.GetComponent<Text>().text);
        float tempY = float.Parse(InputYObject.GetComponent<Text>().text);

        if(tempX < 0)
        {
            simMovement.balanceLocationX = 0;
            StartCoroutine(ShowError(1));
        }

        else if (tempX > 500)
        {
            simMovement.balanceLocationX = 500;
            StartCoroutine(ShowError(1));
        }

        else
        {
            simMovement.balanceLocationX = tempX;
        }

        if (tempY < 0)
        {
            simMovement.balanceLocationY = 0;
            StartCoroutine(ShowError(2));
        }

        else if (tempY > 500)
        {
            simMovement.balanceLocationY = 500;
            StartCoroutine(ShowError(2));
        }

        else
        {
            simMovement.balanceLocationY = tempY;
        }

        simMovement.balancing = true;
    }

    public void balanceSwitchDeactivated()
    {
        StartCoroutine(BalanceSlowly());
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
        StartCoroutine(BalanceSlowly());
        DeactivateAll();
        bounceSwitchDeactivated();
        countdown(24, 1);
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
        StartCoroutine(BalanceSlowly());
        DeactivateAll();
        bounceSwitchDeactivated();
        countdown(20, 2);
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

    public void DeactivateAll()
    {
        buttonTriangleObject.GetComponent<Button>().interactable = false;
        buttonSquareObject.GetComponent<Button>().interactable = false;
        buttonBalanceObject.GetComponent<Button>().interactable = false;
        buttonBounceObject.GetComponent<Button>().interactable = false;
    }

    public void ActivateAll()
    {
        buttonTriangleObject.GetComponent<Button>().interactable = true;
        buttonSquareObject.GetComponent<Button>().interactable = true;
        buttonBalanceObject.GetComponent<Button>().interactable = true;
        buttonBounceObject.GetComponent<Button>().interactable = true;
    }

    public void countdown(int value, int textIndex)
    {
        StartCoroutine(countdownCoroutine(value, textIndex));
    }

    IEnumerator countdownCoroutine(int value, int textIndex)
    {
        Text texter;
        int backup = value;
        if (textIndex == 1)
        {
            texter = Text1Object.GetComponent<Text>();
            Text1Object.SetActive(true);
        }

        else
        {
            texter = Text2Object.GetComponent<Text>();
            Text2Object.SetActive(true);
        }


        for(int i = 0; i < backup; i++)
        {
            texter.text = value.ToString();
            value--;
            yield return new WaitForSeconds(1);
        }
        if (textIndex == 1)
        {
            Text1Object.SetActive(false);
        }

        else
        {
            Text2Object.SetActive(false);
        }
    }

    IEnumerator BalanceSlowly()
    {
        simMovement.balanceMode = 2;
        yield return new WaitForSeconds(2f);
        simMovement.balanceMode = 1;
    }

    IEnumerator ShowError(int errorIndex)
    {
        Text errorText;
        if(errorIndex == 1)
        {
            errorText = Error1Object.GetComponent<Text>();
            errorText.text = "Out of bounds.(0 - 500)";
            Error1Object.SetActive(true);
            yield return new WaitForSeconds(2);
            Error1Object.SetActive(false);
        }
        else
        {
            errorText = Error2Object.GetComponent<Text>();
            errorText.text = "Out of bounds.(0 - 500)";
            Error2Object.SetActive(true);
            yield return new WaitForSeconds(2);
            Error2Object.SetActive(false);
        }
           
    }
}
