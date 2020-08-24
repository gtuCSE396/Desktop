using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConnectionHandler : MonoBehaviour
{
    //string data = ballPositionX + ballPositionY+ ballPositionDistance + motorAngleSouth + motorAngleNorth + motorAngleWest + motorAngleEast
    string exampleData = "250 300 50 10 0 20 0";

    [SerializeField] private GameObject plexiObject;

    private Hard_PlexiMovement hpMovement;

    void Start()
    {
        hpMovement = plexiObject.GetComponent<Hard_PlexiMovement>();
        dataArrived(exampleData);
        StartCoroutine(fourExampleDataArrives());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fourExampleDataArrives()
    {
        yield return new WaitForSeconds(2);
        dataArrived("200 230 15 20 10 0 30");
        yield return new WaitForSeconds(2);
        dataArrived("230 290 40 0 0 10 10");
        yield return new WaitForSeconds(2);
        dataArrived("210 260 30 10 10 0 20");
    }

    public void dataArrived(string data)
    {
        string[] splitArray = data.Split(char.Parse(" "));
        if(splitArray.Count() == 7)
        {
            float positionX = float.Parse(splitArray[0]);
            float positionY = float.Parse(splitArray[1]);
            float positionDistance = float.Parse(splitArray[2]);

            float motorAngleSouth = float.Parse(splitArray[3]);
            float motorAngleNorth = float.Parse(splitArray[4]);
            float motorAngleWest = float.Parse(splitArray[5]);
            float motorAngleEast = float.Parse(splitArray[6]);

            hpMovement.MoveSimulation(positionX, positionY, positionDistance, motorAngleSouth, motorAngleNorth, motorAngleWest, motorAngleEast);
        }
        else
        {
            Debug.Log("Not enough data.");
        }
    }
}
