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
        newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * arcPower;
        newBullet.GetComponent<Rigidbody>().velocity += transform.forward * launchPower;
//         newBullet.GetComponent<Rigidbody>().AddForce( newBullet.transform.forward * launchPower );
    }
}
