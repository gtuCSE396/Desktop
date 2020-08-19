using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongXY : MonoBehaviour
{
    public float customTimer;
    public float xPos = 0f;
    public float yPos = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        customTimer = Time.fixedTime;
        xPos = transform.position.x;
        yPos = transform.position.y;
        transform.position = new Vector3(xPos,yPos,transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        
        if (Time.fixedTime >= customTimer) {
            if (customTimer % 6.0f == 0f) {
                // 1
                xPos = xPos + 80f;
                yPos = yPos - 40f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 1f) {
                // 2
                xPos = xPos + 80f;
                yPos = yPos - 40f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 2f) {
                // 3
                //xPos = xPos + 0f;
                yPos = yPos + 80f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 3f) {
                // 4
                xPos = xPos - 80f;
                yPos = yPos - 40f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 4f) {
                // 5
                xPos = xPos - 80f;
                yPos = yPos - 40f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 5f) {
                // 6
                //xPos = xPos + 0f;
                yPos = yPos + 80f;
                transform.position = new Vector3(xPos,yPos,transform.position.z);
            }

            customTimer = Time.fixedTime + 1.0f;
        }
    }
}
