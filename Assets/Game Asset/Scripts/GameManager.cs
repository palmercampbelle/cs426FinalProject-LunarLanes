using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameEvent
{
    RoundOver,
    MatchOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    // variables
    [SerializeField] private PinGroup m_PinGroup;
    [SerializeField] private MovingPowerBar m_LaunchPowerBar;
    [SerializeField] private BallTracker m_BallTracker;
    [SerializeField] private Scorekeeper m_Scorekeeper;
    [SerializeField] private Player m_Player;
    [SerializeField] private Pause m_Pause;
    [SerializeField] private CanvasGroup m_TitleScreen;
    [SerializeField] private HUDManager m_HUD;

    private BowlingBall m_ActiveBall;

    // accessors
    public MovingPowerBar GetLaunchPowerBar() { return m_LaunchPowerBar; }
    public BallTracker GetBallTracker() { return m_BallTracker; }
    public Scorekeeper GetScorekeeper() { return m_Scorekeeper; }
    public PinGroup GetPinGroup() { return m_PinGroup; }
    public Player GetPlayerScript() { return m_Player; }

    // Awake is called after all objects are initialized, but before Start
    private void Awake()
    {
        //Singleton pattern
        if ( GM == null )
        {
            DontDestroyOnLoad( gameObject );
            GM = this;
        }
        else if ( GM != this )
        {
            Destroy( gameObject );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_TitleScreen.gameObject.SetActive( true );
        m_HUD.SetHidden( true );
        SetPause( true );
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

    public void StartGame()
    {
        m_TitleScreen.gameObject.SetActive( false );
        m_HUD.SetHidden( false );
        SetPause( false );
        RestartGame();
    }

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

    public void SetPause(bool bPause = true)
    {
        if (bPause)
        {
            m_Pause.PauseGame();
        }
        else
        {
            m_Pause.ContinueGame();
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
        switch ( eventType )
        {
        case GameEvent.RoundOver:
            GM.StartNewRound();
            break;

        case GameEvent.MatchOver:
            GM.EndMatch();
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
}
