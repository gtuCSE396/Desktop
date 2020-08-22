using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConnectionHandler : MonoBehaviour
{
    //string data = ballPositionX + ballPositionY+ ballPositionZ + motorAngleSouth + motorAngleNorth + motorAngleWest + motorAngleEast
    string exampleData = "250 300 50 10 0 20 0";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dataArrived(exampleData);
    }

    public void dataArrived(string data)
    {
        string[] splitArray = data.Split(char.Parse(" "));


    }
}
