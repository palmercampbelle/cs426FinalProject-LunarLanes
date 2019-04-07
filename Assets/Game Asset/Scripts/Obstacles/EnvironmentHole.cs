using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject == null )
            return;

        if( collider.gameObject.tag == "ball")
        {
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.tag == "Player" )
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if ( player )
            {
                player.ResetToStart();
            }
        }
    }
}
