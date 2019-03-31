using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinGroup : MonoBehaviour
{
    private List<Pin> pins;

    // Start is called before the first frame update
    void Start()
    {
        pins = new List<Pin>();
        pins.AddRange( GetComponentsInChildren<Pin>() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Pin> GetPins()
    {
        return pins;
    }

    public void ResetPins()
    {
        foreach ( Pin pin in pins )
        {
            pin.ResetToStart();
        }
    }
}
