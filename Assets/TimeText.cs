using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public GameObject TimeTextObject;

    public int hour;
    public int minute;
    public int second;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minute = System.DateTime.Now.Minute;
        second = System.DateTime.Now.Second;
        hour = System.DateTime.Now.Hour;

        TimeTextObject.GetComponent<Text>().text = "TIME : " + hour + ":" + minute + ":" + second;
    }
}
