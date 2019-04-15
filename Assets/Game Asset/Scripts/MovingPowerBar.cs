using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPowerBar : MonoBehaviour
{
    [SerializeField] private SimpleHealthBar m_SimpleBar;
    [SerializeField] private float m_AdjustSpeed = 0.5f;    // change in power per second

    private const float MAX_POWER = 1.0f;

    private float m_CurrPower = 0.0f;
    private bool m_IsMoving = false;

    private void Start()
    {
        ResetPower();
    }

    private void Update()
    {
        if ( m_IsMoving )
        {
            UpdatePower();
        }
    }

    void UpdatePower()
    {
        m_CurrPower += m_AdjustSpeed * Time.deltaTime;
        m_CurrPower = Mathf.Clamp( m_CurrPower, 0.0f, MAX_POWER );
        if ( m_CurrPower == 0 || m_CurrPower == MAX_POWER )
        {
            m_AdjustSpeed = -m_AdjustSpeed;
        }
        m_SimpleBar.UpdateBar( m_CurrPower, MAX_POWER );
    }

    public void SetMoving(bool shouldMove = true ) { m_IsMoving = shouldMove; }
    public bool IsMoving() { return m_IsMoving; }
    public float GetPower() { return m_CurrPower; }

    public void ResetPower()
    {
        if ( m_AdjustSpeed < 0 )
        {
            m_AdjustSpeed = -m_AdjustSpeed;
        }
        m_CurrPower = 0.0f;
        m_SimpleBar.UpdateBar( m_CurrPower, MAX_POWER );
        SetMoving( false );
    }
}
