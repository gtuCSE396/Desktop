using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // another way
        // https://docs.unity3d.com/ScriptReference/Vector3.html
        // Vector3 pos = transform.position;
        // pos.x = anyXval;
        // pos.y = anyYval;
        // transform.position = pos;

        // set min max boundaries of x
        // public float minXpos = sth;
        // public float maxXpos = sth;
        // initialized to middle position x value into var x
        // public float xPos = (((maxXpos+minXpos)/2.0));

        // set min max boundaries of y
        // public float minYpos = sth;
        // public float maxYpos = sth;
        // initialized to middle position y value into var y
        // public float yPos = (((maxYpos+minYpos)/2.0));
        
        // transform.position = new Vector3(xPos,yPos,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // scan new x value into newXpos
        // control boundaries and change position of y to scanned new value
        // if (newXpos < minXpos) xPos.set(minXpos);
        // else if (newXpos > maxXpos) xPos.set(maxXpos);
        // else xPos.set(newXpos);

        // scan new y value into newYpos
        // control boundaries and change position of y to scanned new value
        // if (newYpos < minYpos) yPos.set(minYpos);
        // else if (newYpos > maxYpos) yPos.set(maxYpos);
        // else yPos.set(newYpos);

        // transform.position = new Vector3(xPos,yPos,transform.position.z);
    }
}
