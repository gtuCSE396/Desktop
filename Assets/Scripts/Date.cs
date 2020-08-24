using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Date : MonoBehaviour
{

    public GameObject theTime;
    public int hour;
    public int minutes;
    public int seconds; 
    
    public GameObject theDate;

    public int day;
    public int month;
    public int year;
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;    
        seconds = System.DateTime.Now.Second;
        theTime.GetComponent<Text>().text = "TIME: " + hour + ":" + minutes + ":" + seconds; 

        day = System.DateTime.Now.Day;
        month = System.DateTime.Now.Month;
        year = System.DateTime.Now.Year;
        theDate.GetComponent<Text>().text = "DATE: " + day + ":" + month + ":" + year; 
    }

}
