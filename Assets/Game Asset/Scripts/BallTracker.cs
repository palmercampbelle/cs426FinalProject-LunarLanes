using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracker : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_NumBallsText;
    [SerializeField] private int DEFAULT_BALLS = 3;

    private int m_CurrNumBalls;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrNumBalls = DEFAULT_BALLS;
    }

    // Update is called once per frame
    void Update()
    {
        m_NumBallsText.SetText( m_CurrNumBalls.ToString() );
    }

    public void SetBalls( int numBalls ) { m_CurrNumBalls = numBalls; }
    public void AddBall() { m_CurrNumBalls++; }
    public void LoseBall() { if ( m_CurrNumBalls > 0 ) m_CurrNumBalls--; }
    public bool IsEmpty() { return m_CurrNumBalls == 0; }

    public void ResetToStart() { m_CurrNumBalls = DEFAULT_BALLS; }
}
