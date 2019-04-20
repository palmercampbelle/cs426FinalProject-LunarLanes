using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_PausePanel;
    [SerializeField] private CanvasGroup m_ControlsPanel;

    private bool bPauseMenuOpen = false;
    public bool PauseMenuOpen { get => bPauseMenuOpen; }

    // Start is called before the first frame update
    void Start()
    {
        CloseMenus();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Escape ) && !GameManager.Game.InputDisabled )
        {
            if ( !PauseMenuOpen && !GameManager.Game.IsPaused )
            {
                EnterPauseMenu();
            }
            else if ( PauseMenuOpen )
            {
                ExitPauseMenu();
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////
    /// methods
    /// 

    private void CloseMenus()
    {
        CloseMenu( m_PausePanel );
        CloseMenu( m_ControlsPanel );
    }

    private void CloseMenu( CanvasGroup menuPanel )
    {
        menuPanel.alpha = 0;
        menuPanel.interactable = false;
        menuPanel.blocksRaycasts = false;
    }

    private void OpenMenu( CanvasGroup menuPanel )
    {
        CloseMenus();
        bPauseMenuOpen = true;

        menuPanel.alpha = 1;
        menuPanel.interactable = true;
        menuPanel.blocksRaycasts = true;
    }

    public void EnterPauseMenu()
    {
        ShowPauseMenu();
        GameManager.Game.SetPaused( true );
    }

    public void ExitPauseMenu()
    {
        CloseMenus();
        bPauseMenuOpen = false;
        GameManager.Game.SetPaused( false );
    }

    public void ShowControlsMenu() { OpenMenu( m_ControlsPanel ); }
    public void ShowPauseMenu() { OpenMenu( m_PausePanel ); }
}
