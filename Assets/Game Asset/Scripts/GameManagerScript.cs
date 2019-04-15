using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // variables
    [SerializeField] private PinGroup m_PinGroup;
    [SerializeField] private MovingPowerBar m_LaunchPowerBar;
    [SerializeField] private BallTracker m_BallTracker;
    [SerializeField] private Button m_ResetButton;
    [SerializeField] private Scorekeeper m_Scorekeeper;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        bool bShouldEnableButton = (m_Scorekeeper.GetTotalScore() >= 10 || m_BallTracker.IsEmpty());
        m_ResetButton.interactable = bShouldEnableButton;
    }

    // methods

    GameObject[] GetBalls()
    {
        return GameObject.FindGameObjectsWithTag("ball");
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
        BroadcastMessage( "ResetToStart" );
        m_PinGroup.ResetPins();
    }

    public Button GetResetButton() { return m_ResetButton; }
    public MovingPowerBar GetLaunchPowerBar() { return m_LaunchPowerBar; }
    public BallTracker GetBallTracker() { return m_BallTracker; }
    public Scorekeeper GetScorekeeper() { return m_Scorekeeper; }

    public static GameManagerScript GetGameManager() { return FindObjectOfType<GameManagerScript>(); }
}
