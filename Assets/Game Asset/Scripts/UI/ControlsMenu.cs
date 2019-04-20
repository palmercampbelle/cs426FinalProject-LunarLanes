using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private MenuButton forwardButton;
    [SerializeField] private MenuButton backwardButton;
    [SerializeField] private MenuButton strafeLeftButton;
    [SerializeField] private MenuButton strafeRightButton;
    [SerializeField] private MenuButton turnLeftButton;
    [SerializeField] private MenuButton turnRightButton;
    [SerializeField] private MenuButton aimUpButton;
    [SerializeField] private MenuButton aimDownButton;
    [SerializeField] private MenuButton throwBallButton;

    public const string PLAYER_PREF_FORWARD      = "Forward";     
    public const string PLAYER_PREF_BACKWARD     = "Backward";    
    public const string PLAYER_PREF_STRAFE_LEFT  = "Strafe Left"; 
    public const string PLAYER_PREF_STRAFE_RIGHT = "Strafe Right";
    public const string PLAYER_PREF_TURN_LEFT    = "Turn Left";   
    public const string PLAYER_PREF_TURN_RIGHT   = "Turn Right";  
    public const string PLAYER_PREF_AIM_UP       = "Aim Up";    
    public const string PLAYER_PREF_AIM_DOWN     = "Aim Down";  
    public const string PLAYER_PREF_THROW_BALL   = "Throw Ball";

    List<MenuButton> allControlsButtons;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool bWaitingForInput = false;

    private void Awake()
    {

        forwardButton.LabelText     = PLAYER_PREF_FORWARD;
        backwardButton.LabelText    = PLAYER_PREF_BACKWARD;
        strafeLeftButton.LabelText  = PLAYER_PREF_STRAFE_LEFT;
        strafeRightButton.LabelText = PLAYER_PREF_STRAFE_RIGHT;
        turnLeftButton.LabelText    = PLAYER_PREF_TURN_LEFT;
        turnRightButton.LabelText   = PLAYER_PREF_TURN_RIGHT;
        aimUpButton.LabelText       = PLAYER_PREF_AIM_UP;
        aimDownButton.LabelText     = PLAYER_PREF_AIM_DOWN;
        throwBallButton.LabelText   = PLAYER_PREF_THROW_BALL;

        allControlsButtons = new List<MenuButton>();
        allControlsButtons.Add( forwardButton );
        allControlsButtons.Add( backwardButton );
        allControlsButtons.Add( strafeLeftButton );
        allControlsButtons.Add( strafeRightButton );
        allControlsButtons.Add( turnLeftButton );
        allControlsButtons.Add( turnRightButton );
        allControlsButtons.Add( aimUpButton );
        allControlsButtons.Add( aimDownButton );
        allControlsButtons.Add( throwBallButton );

        // setup the onClick Listeners
        foreach ( MenuButton menuButton in allControlsButtons )
        {
            Button button = menuButton.GetComponent<Button>();
            button.onClick.AddListener( delegate { SendText( button.GetComponentInChildren<Text>() ); } );
            button.onClick.AddListener( delegate { StartAssignment( menuButton.LabelText ); } );
        }
    }

    void Start()
    {
        forwardButton.ButtonText     = GameManager.Controls.forward.ToString();
        backwardButton.ButtonText    = GameManager.Controls.backward.ToString();
        strafeLeftButton.ButtonText  = GameManager.Controls.strafeLeft.ToString();
        strafeRightButton.ButtonText = GameManager.Controls.strafeRight.ToString();
        turnLeftButton.ButtonText    = GameManager.Controls.turnLeft.ToString();
        turnRightButton.ButtonText   = GameManager.Controls.turnRight.ToString();
        aimUpButton.ButtonText       = GameManager.Controls.aimUp.ToString();
        aimDownButton.ButtonText     = GameManager.Controls.aimDown.ToString();
        throwBallButton.ButtonText   = GameManager.Controls.throwBall.ToString();
    }

    void Update()
    {
        foreach ( MenuButton menuButton in GetComponentsInChildren<MenuButton>() )
        {
            menuButton.gameObject.GetComponent<Button>().interactable = !bWaitingForInput;
        }

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if ( canvasGroup )
        {
            canvasGroup.interactable = !bWaitingForInput;
        }
    }

    void OnGUI()
    {
        /*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
        keyEvent = Event.current;

        //Executes if a button gets pressed and
        //the user presses a key
        if ( keyEvent.isKey && bWaitingForInput )
        {
            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
            bWaitingForInput = false;
        }
    }

    /*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
    public void StartAssignment( string keyName )
    {
        if ( !bWaitingForInput )
            StartCoroutine( AssignKey( keyName ) );
    }

    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText( Text text )
    {
        if ( !bWaitingForInput )
            buttonText = text;
    }

    //Used for controlling the flow of our below Coroutine
    IEnumerator WaitForKey()
    {
        while ( bWaitingForInput )
            yield return null;
    }

    /*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
    public IEnumerator AssignKey( string keyName )
    {
        bWaitingForInput = true;

        string tempText = buttonText.text.ToString();
        buttonText.text = "Awaiting Input...";
        yield return WaitForKey(); //Executes endlessly until user presses a key
        buttonText.text = tempText.ToString();

        // player exited pause menu, do nothing
        if ( newKey == KeyCode.Escape )
            yield break;

        switch ( keyName )
        {
        case PLAYER_PREF_FORWARD:
            GameManager.Controls.forward = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_BACKWARD:
            GameManager.Controls.backward = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_STRAFE_LEFT:
            GameManager.Controls.strafeLeft = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_STRAFE_RIGHT:
            GameManager.Controls.strafeRight = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_TURN_LEFT:
            GameManager.Controls.turnLeft = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_TURN_RIGHT:
            GameManager.Controls.turnRight = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_AIM_UP:
            GameManager.Controls.aimUp = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_AIM_DOWN:
            GameManager.Controls.aimDown = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        case PLAYER_PREF_THROW_BALL:
            GameManager.Controls.throwBall = newKey; //Set forward to new keycode
            buttonText.text = newKey.ToString(); //Set button text to new key
            PlayerPrefs.SetString( keyName, newKey.ToString() ); //save new key to PlayerPrefs
            break;
        }

        yield return null;
    }
}
