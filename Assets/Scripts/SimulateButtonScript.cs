using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject SimulateButtonObject;
    [SerializeField] private GameObject StopButtonObject;

  
    public void DisplaySimulate()
    {
        StopButtonObject.SetActive(false);
        SimulateButtonObject.SetActive(true);
    }

    public void DisplayStop()
    {
        SimulateButtonObject.SetActive(false);
        StopButtonObject.SetActive(true);
    }
}
