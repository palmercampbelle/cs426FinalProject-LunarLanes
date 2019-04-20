using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public const int MAX_ROUNDS = 3;

    [SerializeField] private TMPro.TMP_Text m_TotalScoreText;
    [SerializeField] private TMPro.TMP_Text m_RoundScoreText;
    [SerializeField] private TMPro.TMP_Text m_RoundNumText;
    [SerializeField] private TMPro.TMP_Text m_EndingScoreText;

    private int m_TotalScore = 0;
    private int m_RoundScore = 0;
    private int m_CurrentRound = 1;

    public int TotalScore { get => m_TotalScore; }
    public int RoundScore { get => m_RoundScore; }
    public int RoundNum { get => m_CurrentRound; }
    public int ScoreMultiplier { get; set; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_TotalScoreText.SetText( m_TotalScore.ToString() );
        m_EndingScoreText.SetText( m_TotalScore.ToString() );
        m_RoundScoreText.SetText( m_RoundScore.ToString() );
        m_RoundNumText.SetText( m_CurrentRound.ToString() + "/" + MAX_ROUNDS.ToString() );

        if ( IsRoundOver() )
        {
            EndOfRoundChores();
            if ( IsMatchOver() )
            {
                GameManager.SendEvent( GameEvent.MatchOver );
            }
            else
            {
                StartNewRound();
                GameManager.SendEvent( GameEvent.RoundOver );
            }
        }
    }

    private void EndOfRoundChores()
    {
        m_TotalScore += m_RoundScore;
        m_RoundScore = 0;
        ScoreMultiplier = 1;
    }

    public bool IsRoundOver()
    {
        BallTracker ballTracker = GameManager.Game.HUD.mBallTracker;
        if ( ballTracker.IsEmpty() && GameManager.GetBalls().Length == 0 )
        {
            return true;
        }

        return false;
    }
 
    public bool IsMatchOver()
    {
        return m_CurrentRound >= MAX_ROUNDS && IsRoundOver();
    }

    public void StartNewRound()
    {
        m_CurrentRound++;
    }

    public void AddPoints( int points )
    {
        m_RoundScore += points * ScoreMultiplier;
    }

    public void ResetToStart()
    {
        m_CurrentRound = 1;
        m_TotalScore = 0;
        m_RoundScore = 0;
        ScoreMultiplier = 1;
    }
}
