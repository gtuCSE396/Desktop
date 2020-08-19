using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupLog : MonoBehaviour
{
    /*
          if (graphMode == 2)
          {
              logs = new Text[8];

              Text text1 = GameObject.Find("Log1").GetComponent<Text>();
              text1.text = "Mode: Test";
              text1 = GameObject.Find("Log2").GetComponent<Text>();
              logs[0] = text1;
              text1 = GameObject.Find("Log3").GetComponent<Text>();
              logs[1] = text1;
              text1 = GameObject.Find("Log3").GetComponent<Text>();
              logs[2] = text1;
              text1 = GameObject.Find("Log4").GetComponent<Text>();
              logs[3] = text1;
              text1 = GameObject.Find("Log5").GetComponent<Text>();
              logs[4] = text1;
              text1 = GameObject.Find("Log6").GetComponent<Text>();
              logs[5] = text1;
              text1 = GameObject.Find("Log7").GetComponent<Text>();
              logs[6] = text1;
              text1 = GameObject.Find("Log8").GetComponent<Text>();
              logs[7] = text1;
          }
          */

    /* if (graphMode == 2)
           {
               int hour;
               int minutes;
               int seconds;
               hour = System.DateTime.Now.Hour;
               minutes = System.DateTime.Now.Minute;
               seconds = System.DateTime.Now.Second;



               if(logCounter == 8)
               {
                   for(int i = 1; i < 8; i++)
                   {
                       logs[i - 1].text = logs[i].text;
                    }
                   logs[7].text = hour + ":" + minutes + ":" + seconds;
                   logs[7].text += " X = ? " + "Y = " + dHolder.y_values[counter - 1] + " Z = " + dHolder.z_values[counter - 1];
               }
               else
               {
                   logs[logCounter].text = hour + ":" + minutes + ":" + seconds;
                   logs[logCounter].text += " X = ? " + "Y = " + dHolder.y_values[counter - 1] + " Z = " + dHolder.z_values[counter - 1];
                   logCounter++;
               }
           }*/

}
