using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        int currentScore = GetCurrentScore();
        Text score = GetScoreText();
        score.text = "Score: " + currentScore;

        Button resetButton = GetResetButton();
        bool bShouldEnableButton = (currentScore == 10);
        resetButton.interactable = bShouldEnableButton;
        
    }

    // variables
    public Pin pinPrefab;

    // methods

    GameObject[] GetPinSpawns()
    {
        return GameObject.FindGameObjectsWithTag("pinSpawn");
    }

    Pin[] GetPinObjects()
    {
        return GameObject.FindObjectsOfType<Pin>();
    }

    GameObject[] GetBalls()
    {
        return GameObject.FindGameObjectsWithTag("ball");
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

    public void Cleanup()
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pinObject");
        foreach(GameObject pin in pins )
        {
            Destroy(pin);
        }

        GameObject[] balls = GetBalls();
        foreach(GameObject ball in balls)
        {
            Destroy(ball);
        }

    }

    public void RestartGame()
    {
        Cleanup();

        GameObject[] pinspawns = GetPinSpawns();
        foreach( GameObject spawn in pinspawns )
        {
            Instantiate(pinPrefab, spawn.GetComponent<Transform>().position, Quaternion.identity);
            MeshRenderer render = spawn.GetComponentInChildren<MeshRenderer>();
            render.enabled = false;
        }
    }

    public static Text GetScoreText()
    {
        GameObject gameText = GameObject.FindGameObjectWithTag("playerScore");
        Text scoreText = gameText.GetComponent<Text>();
        return scoreText;
    }

    public static Button GetResetButton()
    {
        GameObject gameButton = GameObject.FindGameObjectWithTag("playerResetButton");
        Button resetButton = gameButton.GetComponent<Button>();
        return resetButton;
    }
}
