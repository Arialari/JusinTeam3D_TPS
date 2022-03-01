using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPBuildingGate : MonoBehaviour, IInteractE
{
    public Transform m_NpcTargetTransform;
    public BossAI m_BossAI;
    public EnemyRandomSpawner m_Spawner;
    public GameObject m_Helicopter;
    private SciFiDoor m_SciFiDoor;
    private NpcAI m_NPC;
    void Awake()
    {
        TryGetComponent(out m_SciFiDoor);
        GameObject npc = GameObject.Find("NPC");
        npc.TryGetComponent(out m_NPC);
    }

    // Update is called once per frame
    void Update()
    {
        if(ExtractionQuestManager.GetInstance().GetMission() == ExtractionQuestManager.Mission.Boss_Fight)
        {
            if (m_NPC.transform.position.x > transform.position.x)
            {
                if (!m_SciFiDoor.IsClosed())
                {
                    m_SciFiDoor.ToggleDoor();
                }
            }
        }
    }

    public void InteractE()
    {
        ExtractionQuestManager.Mission mission = ExtractionQuestManager.GetInstance().GetMission();
        if (mission == ExtractionQuestManager.Mission.Get_To_Building)
        {
            m_NPC.SetDestination(m_NpcTargetTransform);
            m_BossAI.Alert();
            m_Spawner.Spawn();
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Get_To_Building);
            m_SciFiDoor.ToggleDoor();
        }
        else if(mission == ExtractionQuestManager.Mission.Try_Extract)
        {
            m_NPC.SetDestination(m_Helicopter.transform);
            m_SciFiDoor.ToggleDoor();
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Try_Extract);
        }
    }
}
