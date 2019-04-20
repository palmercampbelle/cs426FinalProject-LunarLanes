using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private GameObject ball = null;

    private float currentAim = 0.0f;

    [SerializeField] private float AIM_ANGLE_MIN = -65.0f;
    [SerializeField] private float AIM_ANGLE_MAX = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LaunchBall( float launchPower )
    {
        if ( ball != null )
        {
            Destroy( ball );
        }

        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
        ProjectBall( newBall, launchPower );

        ball = newBall;

        GameManager.GM.SetActiveBall( ball.GetComponent<BowlingBall>() );
    }

    public static void ProjectBall( GameObject ball, float launchPower )
    {
        ball.GetComponent<Rigidbody>().velocity += ball.transform.forward * launchPower;
    }

    public void AdjustAim( float degrees )
    {
        currentAim += degrees;
        currentAim = Mathf.Clamp( currentAim, AIM_ANGLE_MIN, AIM_ANGLE_MAX );

        transform.localRotation = Quaternion.Euler( currentAim, 0, 0 );
    }

    public void SetHidden( bool hidden )
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach ( MeshRenderer meshRenderer in meshRenderers )
        {
            meshRenderer.enabled = !hidden;
        }
    }

    public void ResetToStart()
    {
        Destroy( ball );
//         transform.localRotation = Quaternion.identity;
    }
}
