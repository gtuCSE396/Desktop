using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlexiMovement : MonoBehaviour
{

    [SerializeField] private GameObject ballObject;
    private Transform tForm;
    private Rigidbody rBody;
    private bool moveUp;
    private bool moveDown;
    private float vibrationTimer;
    private float rotateTimer;

    private Transform ballTransform;
    private Rigidbody ballRigid;

    private void Awake()
    {

        ballTransform = ballObject.GetComponent<Transform>();
        ballRigid = ballObject.GetComponent<Rigidbody>();
        vibrationTimer = Time.fixedTime;
        rotateTimer = Time.fixedTime;
        moveDown = true;
        moveUp = false;
        tForm = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        if (Time.fixedTime >= vibrationTimer)
        {
            // Switch direction
            if (moveDown)
            {
                moveDown = false;
                moveUp = true;
            }
            else
            {
                moveDown = true;
                moveUp = false;
            }

            // Do the vibration
            if (moveUp)
            {
                Debug.Log("Move up!");
                rBody.AddForce(new Vector3(0, 5f), ForceMode.VelocityChange);
            }
            else if (moveDown)
            {
                Debug.Log("Move down!");
                rBody.AddForce(rBody.velocity * -2, ForceMode.VelocityChange);
            }

            vibrationTimer = Time.fixedTime + 0.05f;
        }
        if (Time.fixedTime >= rotateTimer)
        {
            
            // Balance the ball by velocity
            
            if (ballRigid.velocity.x > 0)
            {
                transform.Rotate(0, 0, 0.01f);
            }
            if (ballRigid.velocity.x < 0)
            {
                transform.Rotate(0, 0, -0.01f);
            }

            if (ballRigid.velocity.z > 0)
            {
                transform.Rotate(-0.01f, 0, 0);
            }

            if (ballRigid.velocity.z < 0)
            {
                transform.Rotate(0.01f, 0, 0);
            }
            
            // Balance the ball by position
    
            if (ballTransform.position.x > transform.position.x)
            {
                transform.Rotate(0, 0, 0.003f);
            }

            if (ballTransform.position.x < transform.position.x)
            {
                transform.Rotate(0, 0, -0.003f);
            }

            if (ballTransform.position.z > transform.position.z)
            {
                transform.Rotate(-0.003f, 0, 0);
            }

            if (ballTransform.position.z < transform.position.z)
            {
                transform.Rotate(0.003f, 0, 0);
            }

            rotateTimer = Time.fixedTime + 0.02f;
        }
    }
}
