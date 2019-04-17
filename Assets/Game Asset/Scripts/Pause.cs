using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private MonoBehaviour[] m_PausableScripts;

    private bool bIsPaused = false;

    void Start()
    {
        m_PausePanel.SetActive( false );
        m_GameOverPanel.SetActive( false );
    }

    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
        {
            if ( !bIsPaused )
            {
                PauseMenu();
            }
            else if ( m_PausePanel.activeInHierarchy )
            {
                ContinueGame();
            }
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

    public void PauseMenu()
    {
        m_PausePanel.SetActive( true );
        PauseGame();
    }

    public void GameOverMenu()
    {
        m_GameOverPanel.SetActive( true );
        PauseGame();
    }

    public void ContinueGame()
    {
        bIsPaused = false;
        Time.timeScale = 1;
        m_PausePanel.SetActive( false );
        m_GameOverPanel.SetActive( false );
        //enable the scripts again

        foreach ( MonoBehaviour script in m_PausableScripts )
        {
            script.enabled = true;
        }
    }

    public bool IsPaused()
    {
        return bIsPaused;
    }
}