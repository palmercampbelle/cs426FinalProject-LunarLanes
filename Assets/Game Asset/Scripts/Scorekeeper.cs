using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_ScoreText;

    private int m_TotalScore = 0;
    private int m_ScoreMultiplier = 1;

    public int GetTotalScore() { return m_TotalScore; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.SetText( m_TotalScore.ToString() );
    }

    public void AddPoints( int points )
    {
        m_TotalScore += points * m_ScoreMultiplier;
    }

    public void ResetToStart()
    {
        m_TotalScore = 0;
    }
}
