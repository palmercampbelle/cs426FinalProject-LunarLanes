using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameEvent
{
    RoundOver,
    MatchOver,
}

public class GameManagerScript : MonoBehaviour
{
    // variables
    [SerializeField] private PinGroup m_PinGroup;
    [SerializeField] private MovingPowerBar m_LaunchPowerBar;
    [SerializeField] private BallTracker m_BallTracker;
    [SerializeField] private Scorekeeper m_Scorekeeper;
    [SerializeField] private Player m_Player;
    [SerializeField] private Pause m_Pause;

    private BowlingBall m_ActiveBall;

    // Start is called before the first frame update
    void Start()
    {
        Invoke( "StartNewRound", 1 );
        //Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        if ( m_PinGroup.CountStandingPins() == 0 )
        {
            Invoke( "ResetPinsAfterSpare", 1 );
        }
    }

    // methods

    public static GameObject[] GetBalls()
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

    private void ResetPinsAfterSpare()
    {
        m_PinGroup.ResetPins();
    }

    public void RestartGame()
    {
        Cleanup();
        BroadcastMessage( "ResetToStart" );
        StartNewRound();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndMatch()
    {
        m_Pause.GameOverMenu();
    }

    public void StartNewRound()
    {
        m_PinGroup.ResetPins();
        m_BallTracker.ResetToStart();
        Cleanup();

        NewRoundMessage newRoundMessage = FindObjectOfType<NewRoundMessage>();
        if ( newRoundMessage != null )
        {
            newRoundMessage.ShowNewRound( m_Scorekeeper.GetRoundNum() );
        }
    }

    public static void SendEvent( GameEvent eventType )
    {
        GameManagerScript gameManager = GetGameManager();
        switch ( eventType )
        {
        case GameEvent.RoundOver:
            gameManager.StartNewRound();
            break;

        case GameEvent.MatchOver:
            gameManager.EndMatch();
            break;

        default:
            break;
        }
    }

    public void SetActiveBall( BowlingBall ball )
    {
        m_ActiveBall = ball;
    }

    public GameObject GetActiveBallObj()
    {
        if ( m_ActiveBall == null )
        {
            return null;
        }

        return m_ActiveBall.gameObject;
    }

    public MovingPowerBar GetLaunchPowerBar() { return m_LaunchPowerBar; }
    public BallTracker GetBallTracker() { return m_BallTracker; }
    public Scorekeeper GetScorekeeper() { return m_Scorekeeper; }
    public PinGroup GetPinGroup() { return m_PinGroup; }

    public static GameManagerScript GetGameManager() { return FindObjectOfType<GameManagerScript>(); }
    public static Player GetPlayerScript() { return GetGameManager().m_Player; }
}
