using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private const float STANDING_THRESHOLD = 30.0f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool bHasFallen = false;

    public bool HasFallen() { return bHasFallen; }

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ( !bHasFallen && !IsStanding() )
        {
            FallDownChores();
        }
    }

    private void FallDownChores()
    {
        ScoreManager scorekeeper = GameManager.Game.HUD.mScoreManager;

        scorekeeper.AddPoints( 1 );

        bHasFallen = true;
        AudioManager.AM.PlayAudioClip( "pin fall", new Vector3(-16.8f, -0.01f, -20.8f) );
    }

    public void ResetToStart()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
//         if ( rigidbody.velocity.magnitude < .01 )
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        transform.position = startPosition;
        transform.rotation = startRotation;
        bHasFallen = false;
    }

    public bool IsStanding()
    {
        //Debug.Log( "Pin y: " + transform.eulerAngles.y );
        bool bIsStanding = transform.eulerAngles.y <= STANDING_THRESHOLD;
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
