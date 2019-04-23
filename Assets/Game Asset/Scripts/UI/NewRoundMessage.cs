using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoundMessage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_RoundNumText;

    private CanvasGroup m_CanvasGroup;

    private float m_FadeSpeed = 0.7f;
    private int m_FadeSteps = 10;
    private float m_MessageLifespan = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNewRound( int roundNum )
    {
        StopAllCoroutines();
        m_CanvasGroup.alpha = 0;
        m_RoundNumText.SetText( roundNum.ToString() );
        StartCoroutine( ShowMessage() );
    }

    private IEnumerator ShowMessage()
    {
        float newAlpha = m_CanvasGroup.alpha;
        float timeStep = m_FadeSpeed / m_FadeSteps;
        float fadeStep = 1.0f / m_FadeSteps;

        // fade in
        while ( newAlpha < 1.0f )
        {
            newAlpha += fadeStep;
            m_CanvasGroup.alpha = Mathf.Clamp( newAlpha, 0, 1 );
            yield return new WaitForSeconds( timeStep );
        }

        // wait
        yield return new WaitForSeconds( m_MessageLifespan );

        //fade out
        while ( newAlpha > 0.0f )
        {
            newAlpha += -fadeStep;
            m_CanvasGroup.alpha = Mathf.Clamp( newAlpha, 0, 1 );
            yield return new WaitForSeconds( timeStep );
        }
    }

}
