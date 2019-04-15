using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour
{
    public const int MAX_ROUNDS = 3;

    [SerializeField] private TMPro.TMP_Text m_TotalScoreText;
    [SerializeField] private TMPro.TMP_Text m_RoundScoreText;
    [SerializeField] private TMPro.TMP_Text m_RoundNumText;

    private int m_TotalScore = 0;
    private int m_RoundScore = 0;
    private int m_ScoreMultiplier = 1;

    private int m_CurrentRound = 1;

    public int GetTotalScore() { return m_TotalScore; }
    public int GetRoundScore() { return m_RoundScore; }
    public int GetRoundNum() { return m_CurrentRound; }

    public void SetMultiplier( int multiplier ) { m_ScoreMultiplier = multiplier; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_TotalScoreText.SetText( m_TotalScore.ToString() );
        m_RoundScoreText.SetText( m_RoundScore.ToString() );
        m_RoundNumText.SetText( m_CurrentRound.ToString() );

        if ( IsRoundOver() )
        {
            if ( IsMatchOver() )
            {
                GameManagerScript.SendEvent( GameEvent.MatchOver );
            }
            else
            {
                GameManagerScript.SendEvent( GameEvent.RoundOver );
                StartNewRound();
            }
        }
    }

    public bool IsRoundOver()
    {
        BallTracker ballTracker = GameManagerScript.GetGameManager().GetBallTracker();
        if ( ballTracker.IsEmpty() && GameManagerScript.GetBalls().Length == 0 )
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

        m_TotalScore += m_RoundScore;
        m_RoundScore = 0;
        m_ScoreMultiplier = 1;
    }

    public void AddPoints( int points )
    {
        m_RoundScore += points * m_ScoreMultiplier;
    }

    public void ResetToStart()
    {
        m_CurrentRound = 1;
        m_TotalScore = 0;
        m_RoundScore = 0;
        m_ScoreMultiplier = 1;
    }
}
