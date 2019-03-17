using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float walkSpeed = 25.0f;
    public float strafeSpeed = 15.0f;
    public float rotationSpeed = 90.0f;
    [SerializeField] private float bulletArc;
    [SerializeField] private float bulletSpeed;


    public GameObject cannonObj;

    BallLauncher cannon;
    Rigidbody rb;
    Transform t;

    // Use this for initialization
    void Start()
    {
        cannon = cannonObj.GetComponent<BallLauncher>();
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // walk
        if ( Input.GetKey( KeyCode.W ) )
            rb.velocity += this.transform.forward * walkSpeed * Time.deltaTime;
        else if ( Input.GetKey( KeyCode.S ) )
            rb.velocity -= this.transform.forward * walkSpeed * Time.deltaTime;

        // strafe
        if ( Input.GetKey( KeyCode.Q ) )
            rb.velocity -= this.transform.right * strafeSpeed * Time.deltaTime;
        else if ( Input.GetKey( KeyCode.E ) )
            rb.velocity += this.transform.right * strafeSpeed * Time.deltaTime;

        // turn
        if ( Input.GetKey( KeyCode.D ) )
            rb.rotation *= Quaternion.Euler( 0, rotationSpeed * Time.deltaTime, 0 );
        else if ( Input.GetKey( KeyCode.A ) )
            rb.rotation *= Quaternion.Euler( 0, -rotationSpeed * Time.deltaTime, 0 );

        // shoot
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            cannon.LaunchBall( bulletArc, bulletSpeed );
        }
    }
}