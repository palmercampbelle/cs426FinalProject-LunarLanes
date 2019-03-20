using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

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
        GameObject newBullet = GameObject.Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
        ProjectBall(newBullet, arcPower, launchPower);
    }

    public static void ProjectBall(GameObject ball, float arcPower, float launchPower)
    {
        ball.GetComponent<Rigidbody>().velocity += Vector3.up * arcPower;
        ball.GetComponent<Rigidbody>().velocity += ball.transform.forward * launchPower;
        //         ball.GetComponent<Rigidbody>().AddForce( newBullet.transform.forward * launchPower );
    }
}
