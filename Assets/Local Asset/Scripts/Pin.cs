﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float standingThreshold = 0.6f;

    public bool IsStanding()
    {
        bool bIsStanding = ( transform.up.y < standingThreshold );
        return bIsStanding;
    }
}
