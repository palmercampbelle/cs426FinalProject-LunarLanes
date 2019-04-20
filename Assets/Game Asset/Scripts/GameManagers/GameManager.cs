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
    public struct ControlScheme
    {
        public KeyCode forward, backward;
        public KeyCode strafeLeft, strafeRight;
        public KeyCode turnLeft, turnRight;
        public KeyCode aimUp, aimDown;
        public KeyCode throwBall;
    }

    public static GameManager Game;
    public static ControlScheme Controls;

    // variables
    [SerializeField] private PinGroup m_PinGroup;
    [SerializeField] private Player m_Player;
    [SerializeField] private CanvasGroup m_TitleScreen;
    [SerializeField] private HUDManager m_HUD;
    [SerializeField] private GameObject m_GameOverPanel;

    private List<MonoBehaviour> m_PausableScripts;
    private bool bIsPaused = false;
    public bool IsPaused { get => bIsPaused; }

    private BowlingBall m_ActiveBall;

    ////////////////////////////////////////////////////////////////////////
    // accessors
    public HUDManager HUD { get => m_HUD; }
    public Player mPlayer { get => m_Player; }
    public PinGroup mPinGroup { get => m_PinGroup; }

    public bool InputDisabled { get; set; } = false;

    ////////////////////////////////////////////////////////////////////////
    /// MonoBehaviour calls

    // Awake is called after all objects are initialized, but before Start
    private void Awake()
    {
        //Singleton pattern
        if ( Game == null )
        {
            DontDestroyOnLoad( gameObject );
            Game = this;
        }
        else if ( Game != this )
        {
            Destroy( gameObject );
        }

        // load player control preferences
        Controls.forward     = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_FORWARD     , KeyCode.W.ToString() ) );
        Controls.backward    = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_BACKWARD    , KeyCode.S.ToString() ) );
        Controls.strafeLeft  = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_STRAFE_LEFT , KeyCode.LeftArrow.ToString() ) );
        Controls.strafeRight = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_STRAFE_RIGHT, KeyCode.RightArrow.ToString() ) );
        Controls.turnLeft    = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_TURN_LEFT   , KeyCode.A.ToString() ) );
        Controls.turnRight   = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_TURN_RIGHT  , KeyCode.D.ToString() ) );
        Controls.aimUp       = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_AIM_UP      , KeyCode.UpArrow.ToString() ) );
        Controls.aimDown     = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_AIM_DOWN    , KeyCode.DownArrow.ToString() ) );
        Controls.throwBall   = (KeyCode)System.Enum.Parse( typeof( KeyCode ), PlayerPrefs.GetString( ControlsMenu.PLAYER_PREF_THROW_BALL  , KeyCode.Space.ToString() ) );

        m_PausableScripts = new List<MonoBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_TitleScreen.gameObject.SetActive( true );
        m_HUD.SetHidden( true );
        SetPaused( true );
        //Debug.Log("Hello World");

        m_GameOverPanel.SetActive( false );

    }

    // Update is called once per frame
    void Update()
    {
        if ( m_PinGroup.CountStandingPins() == 0 )
        {
            Invoke( "ResetPinsAfterSpare", 1 );
        }
    }

    ////////////////////////////////////////////////////////////////////////
    // methods

    public void StartGame()
    {
        m_TitleScreen.gameObject.SetActive( false );
        m_HUD.SetHidden( false );
        SetPaused( false );
        RestartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
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

    ////////////////////////////////////////////////////////////////////////
    /// Pause Interface

    public void SetPaused( bool bPause = true )
    {
        if ( bPause )
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        bIsPaused = true;
        Time.timeScale = 0;

        //Disable scripts that still work while timescale is set to 0
        foreach ( MonoBehaviour script in m_PausableScripts )
        {
            script.enabled = false;
        }
    }

    public void ContinueGame()
    {
        bIsPaused = false;
        Time.timeScale = 1;

        //enable the scripts again
        foreach ( MonoBehaviour script in m_PausableScripts )
        {
            script.enabled = true;
        }
    }

    public void RegisterPausableScript( MonoBehaviour script )
    {
        if ( !m_PausableScripts.Contains( script ) )
        {
            m_PausableScripts.Add( script );
        }
    }

    ////////////////////////////////////////////////////////////////////////
    /// Game Choreo

    private void ResetPinsAfterSpare()
    {
        m_PinGroup.ResetPins();
    }

    public void RestartGame()
    {
        Cleanup();
        m_HUD.ResetToStart();
        StartNewRound();
    }

    private void EndMatch()
    {
        ShowGameOverMenu();
    }

    public void StartNewRound()
    {
        m_PinGroup.ResetPins();
        HUD.mBallTracker.ResetToStart();
        Cleanup();

        NewRoundMessage newRoundMessage = FindObjectOfType<NewRoundMessage>();
        if ( newRoundMessage != null )
        {
            newRoundMessage.ShowNewRound( HUD.mScoreManager.RoundNum );
        }
    }

    ////////////////////////////////////////////////////////////////////////
    /// Game Over
    private void ShowGameOverMenu()
    {
        m_GameOverPanel.SetActive( true );
        SetPaused( true );
    }

    public void HideGameOverMenu()
    {
        m_GameOverPanel.SetActive( false );
    }

    ////////////////////////////////////////////////////////////////////////
    /// Game Events
    public static void SendEvent( GameEvent eventType )
    {
        switch ( eventType )
        {
        case GameEvent.RoundOver:
            Game.StartNewRound();
            break;

        case GameEvent.MatchOver:
            Game.EndMatch();
            break;

        default:
            break;
        }
    }
}
