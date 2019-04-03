using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator m_anim;
    private CharacterController m_controller;
    private BallLauncher m_launcher;

    [SerializeField] private float bulletArc;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float speed = 600.0f;
    [SerializeField] private float turnSpeed = 400.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private const float THROW_DELAY = 0.5f;
    [SerializeField] private const float RETURN_DELAY = 3.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool bHasBall = true;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioManager audioManager; 

    void Start()
    {
        m_launcher = GetComponentInChildren<BallLauncher>();
        m_controller = GetComponent<CharacterController>();
        m_anim = gameObject.GetComponentInChildren<Animator>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void LaunchBall()
    {
        m_launcher.LaunchBall( bulletArc, bulletSpeed );
        bHasBall = false;
        m_anim.SetBool( "HasBall", bHasBall );

        Invoke( "ReturnBall", RETURN_DELAY );
    }

    private void ThrowBall()
    {
        if ( bHasBall )
        {
            m_anim.SetTrigger( "ThrowBall" );
            Invoke( "LaunchBall", THROW_DELAY );
        }
    }

    public void ReturnBall()
    {
        bHasBall = true;
        m_anim.SetBool( "HasBall", bHasBall );
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    void Update()
    {
        // throw ball
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            ThrowBall();
        }

        // turn
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate( 0, turn * turnSpeed * Time.deltaTime, 0 );
        
        // run
        if ( Input.GetKey( KeyCode.W ) )
        {
            m_anim.SetBool( "IsRunning", true );
            audioManager.PlayAudioClip("footstep", transform.position);
            
        }
        else
        {
            m_anim.SetBool( "IsRunning", false );
        }

        if ( m_controller.isGrounded )
        {
            moveDirection = transform.forward * Input.GetAxis( "Vertical" ) * speed;
        }

        m_controller.Move( moveDirection * Time.deltaTime );

        // fall
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
