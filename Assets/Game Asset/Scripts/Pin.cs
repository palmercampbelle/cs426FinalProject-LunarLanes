﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private float standingThreshold = 0.6f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetToStart()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if ( rigidbody.velocity.magnitude < .01 )
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    public bool IsStanding()
    {
        Debug.Log( "Pin y: " + transform.eulerAngles.y );
        bool bIsStanding = transform.eulerAngles.y <= standingThreshold;
        return bIsStanding;
    }

    void OnCollisionEnter(Collision collision)
    {
//         Rigidbody rigidbody = GetComponent<Rigidbody>();
//         rigidbody.isKinematic = false;
// 
//         if(collision.gameObject != null && collision.gameObject.tag == "ball")
//         {
//             BallLauncher.ProjectBall(collision.gameObject, 20.2f, 20.2f);
//         }
    }
}