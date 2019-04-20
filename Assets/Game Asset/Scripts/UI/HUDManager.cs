using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // Fields
    [SerializeField] private MovingPowerBar m_LaunchPowerBar;
    [SerializeField] private BallTracker m_BallTracker;
    [SerializeField] private ScoreManager m_ScoreManager;

    // Accessors
    public MovingPowerBar mLaunchPowerBar { get => m_LaunchPowerBar; }
    public BallTracker mBallTracker { get => m_BallTracker; }
    public ScoreManager mScoreManager { get => m_ScoreManager; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHidden(bool bHidden)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = bHidden ? 0 : 1;
    }

    public void ResetToStart()
    {
        mLaunchPowerBar.ResetPower();
        mBallTracker.ResetToStart();
        mScoreManager.ResetToStart();
    }
}
