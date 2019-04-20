using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Text m_LabelText;
    public string LabelText
    {
        get => m_LabelText != null ? m_LabelText.text : null;
        set => SetLabelText( value );
    }

    Text m_ButtonText;
    public string ButtonText
    {
        get => m_ButtonText.text;
        set => m_ButtonText.text = value;
    }

    private void Awake()
    {
        m_ButtonText = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLabelText( string text )
    {
        if ( m_LabelText != null )
        {
            m_LabelText.text = text;
        }
    }
}
