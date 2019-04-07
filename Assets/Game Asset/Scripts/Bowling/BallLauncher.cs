using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private GameObject ball = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LaunchBall( float arcPower, float launchPower )
    {
        if ( ball != null )
        {
            Destroy( ball );
        }

        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
        ProjectBall( newBall, arcPower, launchPower );

        ball = newBall;
    }

    public static void ProjectBall(GameObject ball, float arcPower, float launchPower)
    {
        ball.GetComponent<Rigidbody>().velocity += Vector3.up * arcPower;
        ball.GetComponent<Rigidbody>().velocity += ball.transform.forward * launchPower;
//         ball.GetComponent<Rigidbody>().AddForce( ball.transform.forward * launchPower );
    }
}
