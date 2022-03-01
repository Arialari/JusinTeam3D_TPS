using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractE
{
    private ExtractionQuestManager.Mission m_eMissionIndex = ExtractionQuestManager.Mission.Open_The_Door2;
    private SciFiDoor m_SciFiDoor;
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
        ExtractionQuestManager.GetInstance().ToNextQuest(m_eMissionIndex);
        if(m_SciFiDoor.m_bIsReadyToOpen)
        {
            m_SciFiDoor.ToggleDoor();
        }
    }
}
