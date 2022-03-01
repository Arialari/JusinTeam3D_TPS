using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPHastageDoor : MonoBehaviour, IInteractE
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
        ExtractionQuestManager.Mission mission = ExtractionQuestManager.GetInstance().GetMission();
        if (mission == ExtractionQuestManager.Mission.Get_To_Defence)
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Get_To_Defence);
        else if (mission >= ExtractionQuestManager.Mission.Get_Hastage)
        {
            m_SmallSciFiDoor.ToggleDoor();
        }
    }
}
