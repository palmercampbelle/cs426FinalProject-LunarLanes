using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // variables
    [SerializeField] private PinGroup m_PinGroup;

    // Start is called before the first frame update
    void Start()
    {
        if (m_PinGroup == null)
        {
            m_PinGroup = FindObjectOfType<PinGroup>();
        }

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

        foreach ( Pin pin in m_PinGroup.GetPins() )
        {
            if ( !pin.IsStanding() )
            {
                ++total;
            }
        }

        return total;
    }

    public void Cleanup()
    {
        GameObject[] balls = GetBalls();
        foreach(GameObject ball in balls)
        {
            Destroy(ball);
        }

    }

    public void RestartGame()
    {
        Cleanup();

        m_PinGroup.ResetPins();
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
