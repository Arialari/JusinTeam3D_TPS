using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPLaptopDoorActivator : MonoBehaviour, IInteractE
{
    public SciFiDoor m_SciFiDoor;
    private ExtractionQuestManager.Mission m_eMission = ExtractionQuestManager.Mission.Get_To_Laptop;
    public void InteractE()
    {
        m_SciFiDoor.m_bIsReadyToOpen = true;
        ExtractionQuestManager.GetInstance().ToNextQuest(m_eMission);
    }
}
