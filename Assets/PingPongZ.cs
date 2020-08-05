using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class PingPongZ : MonoBehaviour
{
    public float customTimer;
    public float yPos = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        customTimer = Time.fixedTime;
        yPos = transform.position.y;
        transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yPos = transform.position.y;
        
        if (Time.fixedTime >= customTimer) {
            if (customTimer % 6.0f == 0f) {
                // Up
                yPos = yPos + 20f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 1f) {
                // Up up
                yPos = yPos + 30f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 2f) {
                // Up up up
                yPos = yPos + 40f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 3f) {
                // Down down down
                yPos = yPos - 40f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 4f) {
                // Down down
                yPos = yPos - 30f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            } else if (customTimer % 6.0f == 5f) {
                // Down
                yPos = yPos - 20f;
                transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
            }

            customTimer = Time.fixedTime + 1.0f;
        }
    }
}
