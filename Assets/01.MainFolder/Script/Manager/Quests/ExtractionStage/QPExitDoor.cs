using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPExitDoor : MonoBehaviour, IInteractE
{
    private SmallSciFiDoor m_SmallSciFiDoor;
    void Start()
    {
        TryGetComponent(out m_SmallSciFiDoor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InteractE()
    {
        ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Get_To_ExitDoor);
        m_SmallSciFiDoor.ToggleDoor();
    }
}
