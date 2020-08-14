using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateText : MonoBehaviour
{
    public GameObject DateTextObject;

    public int day;
    public int month;
    public int year;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        day = System.DateTime.Now.Day;
        month = System.DateTime.Now.Month;
        year = System.DateTime.Now.Year;

        DateTextObject.GetComponent<Text>().text = "DATE : " + day + "/" + month + "/" + year;
    }
}
