using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RestartGame();
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // variables


    // methods

    GameObject[] GetPinSpawns()
    {
        return GameObject.FindGameObjectsWithTag("pinSpawn");
    }

    Pin[] GetPinObjects()
    {
        return GameObject.FindObjectsOfType<Pin>();
    }

    int GetCurrentScore()
    {
        int total = 0;
        Pin[] pinObjects = GetPinObjects();

        foreach(Pin pin in pinObjects)
        {
            if( !pin.IsStanding() )
            {
                ++total;
            }
        }

        return total;
    }

    void RestartGame()
    {
        GameObject[] pinspawns = GetPinSpawns();
        Pin pinPrefab = new Pin();
        foreach( GameObject spawn in pinspawns )
        {
            Instantiate(pinPrefab, spawn.GetComponent<Transform>().position, Quaternion.identity);
            MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
            render.enabled = false;
        }
    }
}
