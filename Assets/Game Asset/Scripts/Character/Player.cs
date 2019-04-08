using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator m_anim;
    private CharacterController m_controller;
    private BallLauncher m_launcher;

    [SerializeField] private Transform camTransform;
    [SerializeField] private float maxBallSpeed;
    [SerializeField] private float minBallSpeed;
    [SerializeField] private float moveSpeed = 600.0f;
    [SerializeField] private float turnSpeed = 400.0f;
    [SerializeField] private float aimSpeed = 200.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private const float THROW_DELAY = 0.5f;
    [SerializeField] private const float RETURN_DELAY = 3.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool bHasBall = true;
    private bool bCanMove = true;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioManager audioManager;
    private GameManagerScript gameManager;

    private float launchPower = 0.0f;
    private float launchPowerAdjustSpeed = 0.5f;    // change in power per second
    private const float MAX_LAUNCH_POWER = 1.0f;

    void Start()
    {
        m_launcher = GetComponentInChildren<BallLauncher>();
        m_controller = GetComponent<CharacterController>();
        m_anim = gameObject.GetComponentInChildren<Animator>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    private void LaunchBall()
    {
        m_launcher.LaunchBall( (maxBallSpeed - minBallSpeed) * launchPower + minBallSpeed );
        ResetLaunchPower();
        bHasBall = false;
        bCanMove = true;
        m_anim.SetBool( "HasBall", bHasBall );
        Invoke( "ReturnBall", RETURN_DELAY );
    }

    private void ThrowBall()
    {
        if ( bHasBall )
        {
            m_anim.SetTrigger( "ThrowBall" );
            Invoke( "LaunchBall", THROW_DELAY );
            audioManager.PlayAudioClip("throw ball", transform.position);
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
        m_launcher.ResetToStart();
    }

    void UpdateLaunchPower()
    {
        SimpleHealthBar launchPowerBar = gameManager.GetLaunchPowerBar();

        launchPower += launchPowerAdjustSpeed * Time.deltaTime;
        launchPower = Mathf.Clamp( launchPower, 0.0f, MAX_LAUNCH_POWER );
        if ( launchPower == 0 || launchPower == MAX_LAUNCH_POWER )
        {
            launchPowerAdjustSpeed = -launchPowerAdjustSpeed;
        }
        launchPowerBar.UpdateBar( launchPower, MAX_LAUNCH_POWER );
    }

    void ResetLaunchPower()
    {
        SimpleHealthBar launchPowerBar = gameManager.GetLaunchPowerBar();

        if ( launchPowerAdjustSpeed < 0 )
        {
            launchPowerAdjustSpeed = -launchPowerAdjustSpeed;
        }
        launchPower = 0.0f;
        launchPowerBar.UpdateBar( launchPower, MAX_LAUNCH_POWER );
    }

    void Update()
    {
        // adjust power of throw by holding down button
        if ( Input.GetButton( "Throw" ) )
        {
            bCanMove = false;
            UpdateLaunchPower();
        }

        // throw ball
        if ( Input.GetButtonUp( "Throw" ) )
        {
            ThrowBall();
        }
        m_launcher.SetHidden( !bHasBall );

        // turn
        float turn = Input.GetAxis( "Turn" );
        transform.Rotate( 0, turn * turnSpeed * Time.deltaTime, 0 );

        // vertical aim
        float aim = Input.GetAxis("Vertical Aim");
        m_launcher.AdjustAim( aim * aimSpeed * Time.deltaTime );

        // walk and strafe
        if ( bCanMove )
        {
            float walkInput = Input.GetAxis( "Walk" );
            float strafeInput = Input.GetAxis( "Strafe" );
            m_anim.SetBool( "RunBackwards", walkInput < 0 );
            m_anim.SetBool( "IsRunning", walkInput != 0 );
            m_anim.SetBool( "IsStrafing", strafeInput != 0 );

            if ( m_controller.isGrounded )
            {
                moveDirection = transform.forward * walkInput * moveSpeed;
                moveDirection += transform.right * strafeInput * moveSpeed;
            }

            if ( walkInput != 0 )
            {
//             audioManager.PlayAudioClip("footstep", transform.position);
            }

            m_controller.Move( moveDirection * Time.deltaTime );
        }
        else
        {
            m_anim.SetBool( "RunBackwards", false );
            m_anim.SetBool( "IsRunning", false );
            m_anim.SetBool( "IsStrafing", false );
        }

        // fall
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
