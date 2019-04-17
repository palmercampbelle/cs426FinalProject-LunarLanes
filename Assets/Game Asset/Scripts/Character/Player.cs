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
    [SerializeField] private float THROW_DELAY = 0.5f;
    [SerializeField] private int SOUND_DELAY = 2;

    private Vector3 moveDirection = Vector3.zero;
    private bool bCanMove = true;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioManager audioManager;
    private GameManagerScript gameManager;

    private int throwBallTriggerHash;
    private int isRunningBoolHash;
    private int isStrafingBoolHash;
    private int runBackwardsBoolHash;
    private int currentTick;

    void Start()
    {
        m_launcher = GetComponentInChildren<BallLauncher>();
        m_controller = GetComponent<CharacterController>();
        m_anim = gameObject.GetComponentInChildren<Animator>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManagerScript>();

        throwBallTriggerHash = Animator.StringToHash( "ThrowBall" );
        isRunningBoolHash    = Animator.StringToHash( "IsRunning" );
        isStrafingBoolHash   = Animator.StringToHash( "IsStrafing" );
        runBackwardsBoolHash = Animator.StringToHash( "RunBackwards" );

        currentTick = 0;
    }

    private void LaunchBall()
    {
        MovingPowerBar launchPowerBar = gameManager.GetLaunchPowerBar();

        m_launcher.LaunchBall( (maxBallSpeed - minBallSpeed) * launchPowerBar.GetPower() + minBallSpeed );
        launchPowerBar.ResetPower();
        gameManager.GetBallTracker().LoseBall();
        bCanMove = true;
    }

    private void ThrowBall()
    {
        if ( HasBall() )
        {
            gameManager.GetLaunchPowerBar().SetMoving( false );
            m_anim.SetTrigger( throwBallTriggerHash );
            Invoke( "LaunchBall", THROW_DELAY );
            audioManager.PlayAudioClip("throw ball", transform.position);
            //audioManager.StopAudioClip("charge");
        }
        else
        {
            bCanMove = true;
            Destroy( gameManager.GetActiveBallObj() );
        }
    }

    private bool HasBall()
    {
        return gameManager.GetActiveBallObj() == null && !gameManager.GetBallTracker().IsEmpty();
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        m_launcher.ResetToStart();
    }

    void Update()
    {
        // adjust power of throw by holding down button
        if ( Input.GetButton( "Throw" ) )
        {
            if ( HasBall() )
            {
                bCanMove = false;
                gameManager.GetLaunchPowerBar().SetMoving( true );
                //audioManager.PlayAudioClip("charge", transform.position);
                //Debug.Log(transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
            }
        }

        // throw ball
        if ( Input.GetButtonUp( "Throw" ) )
        {
            ThrowBall();
        }

        m_launcher.SetHidden( !HasBall() );

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
            m_anim.SetBool( runBackwardsBoolHash, walkInput < 0 );
            m_anim.SetBool( isRunningBoolHash, walkInput != 0 );
            m_anim.SetBool( isStrafingBoolHash, strafeInput != 0 );

            if ( m_controller.isGrounded )
            {
                moveDirection = transform.forward * walkInput * moveSpeed;
                moveDirection += transform.right * strafeInput * moveSpeed;
            }

            if ( walkInput != 0 &&  ( ( ++currentTick ) % SOUND_DELAY == 0) )
            {
                Vector3 offset = new Vector3(10.0f, 10.0f, 10.0f);
                audioManager.PlayAudioClip("footstep", transform.position + offset);
            }

            m_controller.Move( moveDirection * Time.deltaTime );
        }
        else
        {
            m_anim.SetBool( runBackwardsBoolHash, false );
            m_anim.SetBool( isRunningBoolHash, false );
            m_anim.SetBool( isStrafingBoolHash, false );
        }

        // fall
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
