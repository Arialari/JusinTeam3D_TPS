using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoor : MonoBehaviour
{
    private enum State { Opening, Closeing, Stay }
    public GameObject m_Doorleft;
    public GameObject m_DoorRight;
    public bool m_bIsReadyToOpen = true;
    private State m_eState = State.Stay;
    private const float m_fDISTANCE_TO_OPEN = 2.0f;
    private const float m_fDOOR_SPEED = 2.0f;
    private float m_fDoorAlpha = 0.0f;
    private Vector3 m_vInitLeft;
    private Vector3 m_vInitRight;
    void Start()
    {
        m_vInitLeft = m_Doorleft.transform.localPosition;
        m_vInitRight = m_DoorRight.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        switch(m_eState)
        {
            case State.Opening: UpdateOpen();  break;
            case State.Closeing: UpdateClose(); break;
            case State.Stay: return;
        }
        m_Doorleft.transform.localPosition = m_vInitLeft + new Vector3(0f, 0f, m_fDoorAlpha * m_fDISTANCE_TO_OPEN);
        m_DoorRight.transform.localPosition = m_vInitRight + new Vector3(0f, 0f, -m_fDoorAlpha * m_fDISTANCE_TO_OPEN);
    }
    
    private void UpdateOpen()
    {
        m_fDoorAlpha += Time.deltaTime * m_fDOOR_SPEED;
        if(m_fDoorAlpha >= 1.0f)
        {
            m_fDoorAlpha = 1.0f;
            m_eState = State.Stay;
        }
    }

    private void UpdateClose()
    {
        m_fDoorAlpha -= Time.deltaTime * m_fDOOR_SPEED;
        if (m_fDoorAlpha <= 0.0f)
        {
            m_fDoorAlpha = 0.0f;
            m_eState = State.Stay;
        }
    }

    public void ToggleDoor()
    {
        if (!m_bIsReadyToOpen)
        {
            return;
        }

        if(m_eState != State.Stay)
        {
            m_eState = 1 - m_eState;
        }
        else
        {
            m_eState = (State)(m_fDoorAlpha);
        }
    }

    public bool IsClosed()
    {
        return m_fDoorAlpha != 1.0f;
    }

}
