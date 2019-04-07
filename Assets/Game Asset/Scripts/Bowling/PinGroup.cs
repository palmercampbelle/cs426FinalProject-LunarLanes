using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinGroup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Pin[] GetPins()
    {
        return GetComponentsInChildren<Pin>();
    }

    public void ResetPins()
    {
        Pin[] pins = GetPins();
        foreach ( Pin pin in pins )
        {
            pin.ResetToStart();
        }
    }
}
