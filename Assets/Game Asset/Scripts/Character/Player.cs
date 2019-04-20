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
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float aimSpeed;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float THROW_DELAY = 0.5f;
    [SerializeField] private int SOUND_DELAY = 2;

    private Vector3 moveDirection = Vector3.zero;
    private bool bCanMove = true;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private int throwBallTriggerHash;
    private int isRunningBoolHash;
    private int isStrafingBoolHash;
    private int runBackwardsBoolHash;
    private int currentTick;

    private void Awake()
    {
        m_launcher = GetComponentInChildren<BallLauncher>();
        m_controller = GetComponent<CharacterController>();
        m_anim = gameObject.GetComponentInChildren<Animator>();
        
        throwBallTriggerHash = Animator.StringToHash( "ThrowBall" );
        isRunningBoolHash    = Animator.StringToHash( "IsRunning" );
        isStrafingBoolHash   = Animator.StringToHash( "IsStrafing" );
        runBackwardsBoolHash = Animator.StringToHash( "RunBackwards" );

        startPosition = transform.position;
        startRotation = transform.rotation;

        currentTick = 0;
    }

    void Start()
    {
        GameManager.Game.RegisterPausableScript( this );
    }

    private void LaunchBall()
    {
        MovingPowerBar launchPowerBar = GameManager.Game.HUD.mLaunchPowerBar;

        m_launcher.LaunchBall( (maxBallSpeed - minBallSpeed) * launchPowerBar.GetPower() + minBallSpeed );
        launchPowerBar.ResetPower();
        GameManager.Game.HUD.mBallTracker.LoseBall();
        bCanMove = true;
    }

    private void ThrowBall()
    {
        if ( HasBall() )
        {
            GameManager.Game.HUD.mLaunchPowerBar.SetMoving( false );
            m_anim.SetTrigger( throwBallTriggerHash );
            Invoke( "LaunchBall", THROW_DELAY );
            AudioManager.AM.PlayAudioClip("throw ball", transform.position);
            //AudioManager.AM.StopAudioClip("charge");
        }
        else
        {
            bCanMove = true;
            Destroy( GameManager.Game.GetActiveBallObj() );
        }
    }

    private bool HasBall()
    {
        return GameManager.Game.GetActiveBallObj() == null && !GameManager.Game.HUD.mBallTracker.IsEmpty();
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        m_launcher.ResetToStart();
    }

    void Update()
    {
        if ( !GameManager.Game.InputDisabled )
        {
            UpdatePlayerInput();
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

    void UpdatePlayerInput()
    {
        // adjust power of throw by holding down button
        if ( Input.GetKey( GameManager.Controls.throwBall ) )
        {
            if ( HasBall() )
            {
                bCanMove = false;
                GameManager.Game.HUD.mLaunchPowerBar.SetMoving( true );
                //AudioManager.AM.PlayAudioClip("charge", transform.position);
                //Debug.Log(transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
            }
        }

        // throw ball
        if ( Input.GetKeyUp( GameManager.Controls.throwBall ) )
        {
            ThrowBall();
        }

        m_launcher.SetHidden( !HasBall() );

        // turn
        int turn = 0;
        if ( Input.GetKey( GameManager.Controls.turnLeft ) )
        {
            turn = -1;
        }
        else if ( Input.GetKey( GameManager.Controls.turnRight ) )
        {
            turn = 1;
        }
        transform.Rotate( 0, turn * turnSpeed * Time.deltaTime, 0 );

        // vertical aim
        int aim = 0;
        if ( Input.GetKey( GameManager.Controls.aimUp ) )
        {
            aim = -1;
        }
        else if ( Input.GetKey( GameManager.Controls.aimDown ) )
        {
            aim = 1;
        }
        m_launcher.AdjustAim( aim * aimSpeed * Time.deltaTime );

        // walk and strafe
        if ( bCanMove )
        {
            float walkInput = 0;
            if ( Input.GetKey( GameManager.Controls.forward ) )
            {
                walkInput = 1;
            }
            else if ( Input.GetKey( GameManager.Controls.backward ) )
            {
                walkInput = -1;
            }

            float strafeInput = Input.GetAxis( "Strafe" );
            if ( Input.GetKey( GameManager.Controls.strafeLeft ) )
            {
                strafeInput = -1;
            }
            else if ( Input.GetKey( GameManager.Controls.strafeRight ) )
            {
                strafeInput = 1;
            }

            m_anim.SetBool( runBackwardsBoolHash, walkInput < 0 );
            m_anim.SetBool( isRunningBoolHash, walkInput != 0 );
            m_anim.SetBool( isStrafingBoolHash, strafeInput != 0 );

            if ( m_controller.isGrounded )
            {
                moveDirection = transform.forward * walkInput * moveSpeed;
                moveDirection += transform.right * strafeInput * moveSpeed;
            }

            if ( walkInput != 0 && ((++currentTick) % SOUND_DELAY == 0) )
            {
                Vector3 offset = new Vector3(10.0f, 10.0f, 10.0f);
                AudioManager.AM.PlayAudioClip( "footstep", transform.position + offset );
            }

            m_controller.Move( moveDirection * Time.deltaTime );
        }
        else
        {
            m_anim.SetBool( runBackwardsBoolHash, false );
            m_anim.SetBool( isRunningBoolHash, false );
            m_anim.SetBool( isStrafingBoolHash, false );
        }
    }
}
