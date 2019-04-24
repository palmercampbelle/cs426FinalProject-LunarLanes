using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CreditsScreen;
    [SerializeField] private TextMeshProUGUI m_creditsText;
    [SerializeField] private float m_speed = 2.0f;
    [SerializeField] private float m_topTreshold = 0.8f;
    private bool mb_IsMoving = false;

    private Vector3 m_OriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_OriginalPosition = m_creditsText.rectTransform.position;
        StopCredits();
    }

    void SetCredits()
    {
        string credits = "This work was done at the University of Illinois at Chicago as part of computer science 426 (Video Game Design and Implementation)\n";
        credits += "\n";
        credits += "[Design & Programming]\n";
        credits += "Etienne Palmer-Campbell\n";
        credits += "Marquise Howard\n";
        credits += "\n";
        credits += "[Art]\n";
        credits += "Living Brirds: DINOPUNCH (Unity Asset Store)\n";
        credits += "Tank & Healer Studio: Simple Health Bar FREE (Unity Asset Store)\n";
        credits += "Stagit East: Earth and Planets Skyboxes (Unity Asset Store)\n";
        credits += "Bowling ball and pin 3D Model: (https://free3d.com/3d-model/bowling-ball-and-pin-22040.html)\n";
        credits += "MK Toon Free: (Unity Asset Store)";
        credits += "\n";
        credits += "[Sound]\n";
        credits += "Footstep On Wood: SoundsAreGr8 (freesound.org)\n";
        credits += "Bounce: josepharaoh99 (freesound.org)\n";
        credits += "boune percussion thing: waveplay_old (freesound.org)\n";
        credits += "Attack Punch: elynch0901 (freesound.org)\n";
        credits += "charging power: JavierZumer (freesound.org) \n";

        m_creditsText.text = credits;
    }

    public void StopCredits()
    {
        m_CreditsScreen.gameObject.SetActive(false);
        m_creditsText.text = "";
        mb_IsMoving = false;
        m_creditsText.rectTransform.position = m_OriginalPosition;
    }

    public void StartCredits()
    {
        m_CreditsScreen.gameObject.SetActive(true);
        SetCredits();
        mb_IsMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mb_IsMoving)
        {
            Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
            offset *= m_speed;
            m_creditsText.rectTransform.position += offset;

            if( m_creditsText.rectTransform.position.y > m_topTreshold )
            {
                StopCredits();
            }
        }
    }
}
