using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Script : MonoBehaviour
{
    public struct MissionInfo
    {
        public string _strTitle;
        public string _strDetail;

        public MissionInfo(string _Title, string _Detail)
        {
            _strTitle = _Title;
            _strDetail = _Detail;
        }
    }

    public Dictionary<ExtractionQuestManager.Mission, MissionInfo> m_MissionInfo = new Dictionary<ExtractionQuestManager.Mission, MissionInfo>();

    public MainMenuUI m_TabMenu { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Door, new MissionInfo(" - �ǹ� ����", " - �ǹ��� �����϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Laptop, new MissionInfo(" - ��������", " - ��Ʈ���� ����� ������ �����϶�"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Middle, new MissionInfo(" - �߰�����", " - �߰��������� �̵��϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Open_The_Door2, new MissionInfo(" - �غ� �ܰ�", " - ���� �������� �̵��� �غ�����."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Defence, new MissionInfo(" - �ڻ� ����", " - �ڻ縦 ã��."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Defence, new MissionInfo(" - �ڻ� ����", " - �ֺ��� ���� �����϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_Hastage, new MissionInfo(" - �ڻ� ����", " - �ٽ� �ڻ縦 ã�ư���."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_ExitDoor, new MissionInfo(" - ����������..", " - ���������� �ڻ縦 �ȳ��϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Building, new MissionInfo(" - ����������..", " - ���������� �ڻ縦 �ȳ��϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Boss_Fight, new MissionInfo(" - ���� ����ü óġ", " - ���� ����ü�� óġ�϶�"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Try_Extract, new MissionInfo(" - �ڻ� ȣ��", " - �ڻ縦 �ȳ��϶�"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Use_MiniGun, new MissionInfo(" - ���� ����", " - �̴ϰǿ� ž���ض�"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Hold_Until_Extract, new MissionInfo(" - ���", " - �õ��� �ɸ������� ����϶�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Extract, new MissionInfo(" - Ż��", " - ��⿡ ž���ض�."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.END, new MissionInfo(" - �̼� ����", " - ��ȯ�Ѵ�."));
        m_TabMenu = GameObject.Find("Main Menu Panel").GetComponent<MainMenuUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
