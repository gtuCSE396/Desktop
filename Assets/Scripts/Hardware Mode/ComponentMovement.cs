using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentMovement : MonoBehaviour
{
    [SerializeField] GameObject SouthObject;
    [SerializeField] GameObject NorthObject;
    [SerializeField] GameObject EastObject;
    [SerializeField] GameObject WestObject;

    [SerializeField] GameObject ArmSouthObject;
    [SerializeField] GameObject ArmNorthObject;
    [SerializeField] GameObject ArmEastObject;
    [SerializeField] GameObject ArmWestObject;

    private Transform SouthTransform;
    private Transform NorthTransform;
    private Transform EastTransform;
    private Transform WestTransform;

    private Transform ArmSouthTransform;
    private Transform ArmNorthTransform;
    private Transform ArmEastTransform;
    private Transform ArmWestTransform;

    Vector3 SouthInitial;
    Vector3 NorthInitial;
    Vector3 EastInitial;
    Vector3 WestInitial;

    Vector3 ArmSouthInitial;
    Vector3 ArmNorthInitial;
    Vector3 ArmEastInitial;
    Vector3 ArmWestInitial;

    float yVlaueEW = 0.02f;
    float yValue = 0.015f;

    private void Start()
    {
        SouthTransform = SouthObject.GetComponent<Transform>();
        NorthTransform = NorthObject.GetComponent<Transform>();
        EastTransform = EastObject.GetComponent<Transform>();
        WestTransform = WestObject.GetComponent<Transform>();

        ArmSouthTransform = ArmSouthObject.GetComponent<Transform>();
        ArmNorthTransform = ArmNorthObject.GetComponent<Transform>();
        ArmEastTransform = ArmEastObject.GetComponent<Transform>();
        ArmWestTransform = ArmWestObject.GetComponent<Transform>();

        SouthInitial = SouthTransform.position;
        NorthInitial = NorthTransform.position;
        EastInitial =  EastTransform.position;
        WestInitial = WestTransform.position;

        ArmSouthInitial = ArmSouthTransform.position;
        ArmNorthInitial = ArmNorthTransform.position;
        ArmEastInitial = ArmEastTransform.position;
        ArmWestInitial = ArmWestTransform.position;
    }

    public void MoveComponents(float angleSouthNorth, float angleEastWest)
    {
        Debug.Log("Fuck");

        ArmSouthTransform.position = ArmSouthInitial;
        ArmSouthTransform.position = new Vector3(ArmSouthInitial.x, ArmSouthInitial.y + (angleSouthNorth * yValue), ArmSouthInitial.z);

        SouthTransform.position = SouthInitial;
        SouthTransform.position = new Vector3(SouthInitial.x, SouthInitial.y + (angleSouthNorth * yValue), SouthInitial.z);

        ArmNorthTransform.position = ArmNorthInitial;
        ArmNorthTransform.position = new Vector3(ArmNorthInitial.x, ArmNorthInitial.y + (-angleSouthNorth * yValue), (ArmNorthInitial.z));

        NorthTransform.position = NorthInitial;
        NorthTransform.position = new Vector3(NorthInitial.x, NorthInitial.y + (-angleSouthNorth * yValue), NorthInitial.z);

        ArmEastTransform.position = ArmEastInitial;
        ArmEastTransform.position = new Vector3(ArmEastInitial.x, ArmEastInitial.y + (angleEastWest * yVlaueEW), ArmEastInitial.z);

        EastTransform.position = EastInitial;
        EastTransform.position = new Vector3(EastInitial.x, EastInitial.y + (angleEastWest * yVlaueEW), EastInitial.z);

        ArmWestTransform.position = ArmWestInitial;
        ArmWestTransform.position = new Vector3((ArmWestInitial.x), ArmWestInitial.y + (-angleEastWest * yVlaueEW), ArmWestInitial.z);

        WestTransform.position = WestInitial;
        WestTransform.position = new Vector3(WestInitial.x, EastInitial.y + (-angleEastWest * yVlaueEW), WestInitial.z);
    }
}
