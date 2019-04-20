using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    private const float DEAD_BALL_THRESHOLD = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
//         if ( IsBallDead() )
//         {
//             Destroy( gameObject );
//         }
    }

    public bool IsBallDead()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if ( rigidbody.velocity.magnitude < DEAD_BALL_THRESHOLD )
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.AM.PlayAudioClip("ball bounce", transform.position);
    }
}
