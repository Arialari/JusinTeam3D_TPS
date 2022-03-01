using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionQuestManager : MonoBehaviour
{
    public enum Mission { Get_To_Door, Get_To_Laptop, //Hold_Until_Open,
                            Get_To_Middle,
                            Open_The_Door2, Get_To_Defence, Defence, Get_Hastage, Get_To_ExitDoor, Get_To_Building,
                            Boss_Fight, Try_Extract, Use_MiniGun, Hold_Until_Extract,Extract, END}
    public GameObject m_Door1;
    private SciFiDoor m_ScifiDoor1;
    public GameObject m_Door2;
    private SciFiDoor m_ScifiDoor2;
    public GameObject m_MiddleColliderObject;
    public GameObject[] m_ZombieGate = new GameObject[2];
    public EnemyRandomSpawner m_EnemyRandomSpawner;
    public NpcAI m_NpcAI;
    public GameObject m_TimeUIPrefab;
    public QPHeli m_qpHelicopter;

    private GameObject m_Player;
    private Mission m_eMission = Mission.Get_To_Door;
    private Mission m_ePrevMission = Mission.END;
    private static ExtractionQuestManager m_Instance;
    private ExtractionScriptManager m_ScriptManager;
    void Awake()
    {
        m_Instance = this;
        TryGetComponent(out m_ScriptManager);
    }
    void Start()
    {
        m_Player = GameObject.Find("Player");
        m_Door1.TryGetComponent(out m_ScifiDoor1);
        m_Door2.TryGetComponent(out m_ScifiDoor2);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateQuest();
    }

    public Mission GetMission()
    {
        return m_eMission;
    }

    public static ExtractionQuestManager GetInstance()
    {
        return m_Instance;
    }
    public void ToNextQuest(Mission eMissionIndex)
    {
        if (eMissionIndex != m_eMission)
            return;
        ++m_eMission;
        m_ScriptManager.StartScript();
        QuestTargetManager.GetInstance().CompleteCurrentQuest();
        QuestTargetManager.GetInstance().StartNextQuest();
    }

    private void UpdateQuest()
    {
        switch(m_eMission)
        {
            case Mission.Get_To_Door: break; // --
            case Mission.Get_To_Laptop: break; // --
            //case Mission.Hold_Until_Open: break; // --
            case Mission.Get_To_Middle: CheckMiddle(); break; 
            case Mission.Open_The_Door2: CheckDoor2Open(); break;
            case Mission.Get_To_Defence: break;
            case Mission.Defence: CheckEnemyClear();  break;
            case Mission.Get_Hastage: break;
            case Mission.Get_To_ExitDoor: break;
            case Mission.Boss_Fight:
                if (m_eMission != m_ePrevMission)
                {
                    BGMSound.GetInstance().PlayBossBGM();
                }
                    break;
            case Mission.Try_Extract: break;
            case Mission.Use_MiniGun:
                break;
            case Mission.Hold_Until_Extract:
                if (m_eMission != m_ePrevMission)
                {
                    Destroy(m_ZombieGate[0]); Destroy(m_ZombieGate[1]);
                    m_EnemyRandomSpawner.m_iRandomMin = 1;
                    m_EnemyRandomSpawner.m_fSpawnCoolTime = 2f;
                    m_EnemyRandomSpawner.Spawn();

                    Instantiate(m_TimeUIPrefab);

                    BGMSound.GetInstance().PlayWaveBGM();
                }
                break;
            case Mission.Extract: 
                if (m_eMission != m_ePrevMission)
                {
                    m_qpHelicopter.ActiveCollider();
                    m_EnemyRandomSpawner.m_fSpawnCoolTime = 5f;
                }
                    break;
            default: break;
        }
        m_ePrevMission = m_eMission;
    }
    private void CheckMiddle()
    {
        if(m_Player.transform.position.x > m_Door1.transform.position.x && m_ScifiDoor1.IsClosed())
        {
            EnemyManager.GetInstance().DestroyEnemys(CheckLeftOfMiddle);
            ToNextQuest(m_eMission);
        }
    }

    private void CheckDoor2Open()
    {
        if(!m_ScifiDoor2.IsClosed())
        {
            ToNextQuest(m_eMission);
        }
    }

    public bool CheckLeftOfMiddle(GameObject Enemy)
    {
        return Enemy.transform.position.x < m_Door2.transform.position.x;
    }
    public void CheckEnemyClear()
    {
        if (EnemyManager.GetInstance().IsEmpty())
        {
            ToNextQuest(m_eMission);
        }
    }
}
