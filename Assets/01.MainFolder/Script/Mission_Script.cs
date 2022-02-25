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
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Door, new MissionInfo(" - 건물 진입", " - 건물로 진입하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Laptop, new MissionInfo(" - 보안해제", " - 노트북을 사용해 보안을 해제하라"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Middle, new MissionInfo(" - 중간지점", " - 중간지점으로 이동하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Open_The_Door2, new MissionInfo(" - 준비 단계", " - 다음 지점으로 이동전 준비하자."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Defence, new MissionInfo(" - 박사 구출", " - 박사를 찾자."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Defence, new MissionInfo(" - 박사 구출", " - 주변의 적을 섬멸하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_Hastage, new MissionInfo(" - 박사 구출", " - 다시 박사를 찾아가자."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_ExitDoor, new MissionInfo(" - 연구동으로..", " - 연구동으로 박사를 안내하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Get_To_Building, new MissionInfo(" - 연구동으로..", " - 연구동으로 박사를 안내하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Boss_Fight, new MissionInfo(" - 대형 생명체 처치", " - 대형 생명체를 처치하라"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Try_Extract, new MissionInfo(" - 박사 호출", " - 박사를 안내하라"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Use_MiniGun, new MissionInfo(" - 위기 감지", " - 미니건에 탑승해라"));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Hold_Until_Extract, new MissionInfo(" - 방어", " - 시동이 걸릴때까지 방어하라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.Extract, new MissionInfo(" - 탈출", " - 헬기에 탑승해라."));
        m_MissionInfo.Add(ExtractionQuestManager.Mission.END, new MissionInfo(" - 미션 종료", " - 귀환한다."));
        m_TabMenu = GameObject.Find("Main Menu Panel").GetComponent<MainMenuUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
