using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hard_ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject buttonBounceObject;
    [SerializeField] private GameObject buttonBalanceObject;
    [SerializeField] private GameObject buttonSquareObject;
    [SerializeField] private GameObject buttonTriangleObject;
    [SerializeField] private GameObject buttonCircleObject;

    [SerializeField] private GameObject deactivatedObject;
    [SerializeField] private GameObject activatedObject;
    [SerializeField] private GameObject squareRedObject;
    [SerializeField] private GameObject squareGreenObject;
    [SerializeField] private GameObject triangleRedObject;
    [SerializeField] private GameObject triangleGreenObject;

    [SerializeField] private GameObject circleRedObject;
    [SerializeField] private GameObject circleGreenObject;

    [SerializeField] private GameObject plexiObject;
    [SerializeField] private GameObject InputXObject;
    [SerializeField] private GameObject InputYObject;
    [SerializeField] private GameObject InputHObject;

    [SerializeField] private GameObject Text1Object;
    [SerializeField] private GameObject Text2Object;
    [SerializeField] private GameObject Text3Object;

    [SerializeField] private GameObject Error1Object;
    [SerializeField] private GameObject Error2Object;
    [SerializeField] private GameObject Error3Object;

    [SerializeField] private GameObject ClientObject;

    private GameObject cloneBounceDeactivated;
    private GameObject cloneBounceActivated;

    private GameObject cloneBalanceDeactivated;
    private GameObject cloneBalanceActivated;

    private GameObject cloneSquareDeactivated;
    private GameObject cloneSquareActivated;

    private GameObject cloneTriangleDeactivated;
    private GameObject cloneTriangleActivated;

    private GameObject cloneCircleDeactivated;
    private GameObject cloneCircleActivated;

    private ClientSide clientSide;

    private bool bounceActivated;
    private bool balanceActivated;
    private bool squareActivated;
    private bool triangleActivated;
    private bool circleActivated;

    private Sim_PlexiMovement simMovement;

    // Start is called before the first frame update
    void Start()
    {
        cloneBalanceDeactivated = Instantiate(deactivatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        cloneBounceDeactivated = Instantiate(deactivatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        cloneSquareDeactivated = Instantiate(squareRedObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        cloneTriangleDeactivated = Instantiate(triangleRedObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        cloneCircleDeactivated = Instantiate(circleRedObject, buttonCircleObject.transform.position, buttonCircleObject.transform.rotation, buttonCircleObject.transform);

        bounceActivated = false;
        balanceActivated = false;
        squareActivated = false;
        triangleActivated = false;
        circleActivated = false;

        simMovement = plexiObject.GetComponent<Sim_PlexiMovement>();
        clientSide = ClientObject.GetComponent<ClientSide>();

        clientSide.SendWithParameter("Desktop Client Initialized.");
    }

    public void balanceSwitchActivated()
    {
        DeactivateAll();
        buttonBalanceObject.GetComponent<Button>().interactable = true;
        clientSide.SendWithParameter("D 1 0 0");
        Destroy(cloneBalanceDeactivated);
        cloneBalanceActivated = Instantiate(activatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        balanceActivated = true;

    }

    public void balanceSwitchDeactivated()
    {
        ActivateAll();
        clientSide.SendWithParameter("D 0 0 0");
        Destroy(cloneBalanceActivated);
        cloneBalanceDeactivated = Instantiate(deactivatedObject, buttonBalanceObject.transform.position, buttonBalanceObject.transform.rotation, buttonBalanceObject.transform);
        balanceActivated = false;
    }

    public void bounceSwitchActivated()
    {
        if (InputHObject.GetComponent<Text>().text == "")
        {
            StartCoroutine(ShowError(3));
            return;
        }
        DeactivateAll();
        buttonBounceObject.GetComponent<Button>().interactable = true;
        float tempH = float.Parse(InputHObject.GetComponent<Text>().text);
        clientSide.SendWithParameter("D 2 " + Mathf.Round(tempH) + " 0");    // 0 is placeholder

        Destroy(cloneBounceDeactivated);
        cloneBounceActivated = Instantiate(activatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        bounceActivated = true;
    }

    public void bounceSwitchDeactivated()
    {
        ActivateAll();
        clientSide.SendWithParameter("D 0 0 0");
        Destroy(cloneBounceActivated);
        cloneBounceDeactivated = Instantiate(deactivatedObject, buttonBounceObject.transform.position, buttonBounceObject.transform.rotation, buttonBounceObject.transform);
        bounceActivated = false;
    }

    public void squareSwitchActivated()
    {
        clientSide.SendWithParameter("D 3 2 0");
        DeactivateAll();
        countdown(5, 1);
        Destroy(cloneSquareDeactivated);
        cloneSquareActivated = Instantiate(squareGreenObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        squareActivated = true;
    }

    public void squareSwitchDeactivated()
    {
        Destroy(cloneSquareActivated);
        cloneSquareDeactivated = Instantiate(squareRedObject, buttonSquareObject.transform.position, buttonSquareObject.transform.rotation, buttonSquareObject.transform);
        squareActivated = false;
    }

    public void triangleSwitchActivated()
    {
        clientSide.SendWithParameter("D 3 3 0");
        DeactivateAll();
        countdown(5, 2);
        Destroy(cloneTriangleDeactivated);
        cloneTriangleActivated = Instantiate(triangleGreenObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        triangleActivated = true;
    }

    public void triangleSwitchDeactivated()
    {
        Destroy(cloneTriangleActivated);
        cloneTriangleDeactivated = Instantiate(triangleRedObject, buttonTriangleObject.transform.position, buttonTriangleObject.transform.rotation, buttonTriangleObject.transform);
        triangleActivated = false;
    }

    public void circleSwitchActivated()
    {
        clientSide.SendWithParameter("D 3 1 0");
        DeactivateAll();
        countdown(5, 3);
        Destroy(cloneCircleDeactivated);
        cloneCircleActivated = Instantiate(circleGreenObject, buttonCircleObject.transform.position, buttonCircleObject.transform.rotation, buttonCircleObject.transform);
        circleActivated = true;
    }

    public void circleSwitchDeactivated()
    {
        Destroy(cloneCircleActivated);
        cloneCircleDeactivated = Instantiate(circleRedObject, buttonCircleObject.transform.position, buttonCircleObject.transform.rotation, buttonCircleObject.transform);
        circleActivated = false;
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

    public void handleCircle()
    {
        if (circleActivated)
        {
            circleSwitchDeactivated();
        }
        else
        {
            circleSwitchActivated();
        }
    }

    public void DeactivateAll()
    {
        buttonTriangleObject.GetComponent<Button>().interactable = false;
        buttonSquareObject.GetComponent<Button>().interactable = false;
        buttonBalanceObject.GetComponent<Button>().interactable = false;
        buttonBounceObject.GetComponent<Button>().interactable = false;
        buttonCircleObject.GetComponent<Button>().interactable = false;
    }

    public void ActivateAll()
    {
        buttonCircleObject.GetComponent<Button>().interactable = true;
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

        else if(textIndex == 2)
        {
            texter = Text2Object.GetComponent<Text>();
            Text2Object.SetActive(true);
        }

        else
        {
            texter = Text3Object.GetComponent<Text>();
            Text3Object.SetActive(true);
        }


        for (int i = 0; i < backup; i++)
        {
            texter.text = value.ToString();
            value--;
            yield return new WaitForSeconds(1);
        }

        if (textIndex == 1)
        {
            squareSwitchDeactivated();
            Text1Object.SetActive(false);
        }

        else if (textIndex == 2)
        {
            triangleSwitchDeactivated();
            Text2Object.SetActive(false);
        }
        else
        {
            circleSwitchDeactivated();
            Text3Object.SetActive(false);
        }
        ActivateAll();
    }

    IEnumerator ShowError(int errorIndex)
    {
        Text errorText;
        if (errorIndex == 1)
        {
            errorText = Error1Object.GetComponent<Text>();
            errorText.text = "Out of bounds.(0 - 500)";
            Error1Object.SetActive(true);
            yield return new WaitForSeconds(2);
            Error1Object.SetActive(false);
        }
        else if(errorIndex == 2)
        {
            errorText = Error2Object.GetComponent<Text>();
            errorText.text = "Out of bounds.(0 - 500)";
            Error2Object.SetActive(true);
            yield return new WaitForSeconds(2);
            Error2Object.SetActive(false);
        }
        else if (errorIndex == 3)
        {
            errorText = Error3Object.GetComponent<Text>();
            errorText.text = "Enter height.";
            Error3Object.SetActive(true);
            yield return new WaitForSeconds(2);
            Error3Object.SetActive(false);
        }

    }
}
