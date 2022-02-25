using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPDoor1 : MonoBehaviour, IInteractE
{
    private SciFiDoor m_SciFiDoor;
    private bool m_bQuest = true;
    void Awake()
    {
        TryGetComponent(out m_SciFiDoor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractE()
    {
        if(m_bQuest)
        {
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Get_To_Door);
            m_bQuest = false;
        }
        if(m_SciFiDoor.m_bIsReadyToOpen)
        {
            m_SciFiDoor.ToggleDoor();
        }
    }
}
