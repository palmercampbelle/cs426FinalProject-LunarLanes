using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFloor : MonoBehaviour
{
    [SerializeField] private bool fragile = false;
    [SerializeField] private int health = 2;

    private int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter( Collision collision )
    {
        if ( fragile )
        {
            // todo
        }
    }
}
