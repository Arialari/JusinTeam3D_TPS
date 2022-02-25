using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionScriptManager : MonoBehaviour
{
    public Texture m_tPlayer;
    public Texture m_tOperator;
    public Texture m_tNPC;
    public Texture m_tBoss;
    private MainUI_Head m_UIHead;
    private const float m_fLESSTIME = 1.0f;
    private int m_ScriptIndex = 0;
    private struct Script
    {
        public Script(ScriptGroupUI.Speaker _speaker, Texture _texture, string _script)
        {
            speaker = _speaker;
            potrait = _texture;
            script = _script;
        }
        public ScriptGroupUI.Speaker speaker;
        public Texture potrait;
        public string script;
    }
    private Script[][] m_scripts = new Script[(int)ExtractionQuestManager.Mission.END+1][];
    void Awake()
    {
        GameObject UIObject = GameObject.Find("Canvas");
        UIObject.TryGetComponent(out m_UIHead);
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Door] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Door][0] = new Script(ScriptGroupUI.Speaker.Player,
            m_tPlayer, "기체번호 118-A 목표지점 도착했습니다.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Door][1] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "파일럿 운행 가능상태로 변경 되었습니다. 임무를 시작합니다.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop][0] = new Script(ScriptGroupUI.Speaker.Player,
            m_tPlayer, "잠겼군");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop][1] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "보안 문이 잠겨있습니다. 보안 콘솔 좌표를 전송하였습니다. 콘솔 위치로 이동하여 콘솔을 조작 하십시오.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Middle] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Middle][0] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "보안 해제 확인. 안전지대 까지 이동 가능합니다.");

        m_scripts[(int)ExtractionQuestManager.Mission.Open_The_Door2] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Open_The_Door2][0] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "건물 내부 스캔 중..");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Defence] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Defence][0] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "구출 목표 식별 완료. 좌표를 전송합니다.");

        m_scripts[(int)ExtractionQuestManager.Mission.Defence] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "안에 계십니까? 구조대입니다.");
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][1] = new Script(ScriptGroupUI.Speaker.Other,
           m_tNPC, "드디어! 구조대가..!! 신이시여 정말 감사합니다!!");
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][2] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "구출 목표의 안전을 위해 주변 적의 섬멸로 이행하십시오");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "이걸로 끝인가.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "적의 섬멸을 확인. 구출 목표를 확보하십시오.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "후우.. 빨리 이 지옥에서 나가고 싶지만. 옆 동에 백신 연구자료가 있소.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][1] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "이런.. 일이 늘었군. 오퍼레이터?");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][2] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "연구동의 좌표를 전송합니다.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "알려지지 않은 거대 생체반응 확인. 탈출루트를 확보하십시오.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][1] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "박사는 연구동으로 가시오. 자료를 다 챙기더라도 내가 문을 열때까지 나오지 마시오.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][2] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "아.. 알겠소.");

        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "이리와. 한판 붙어보자고");
        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tBoss, "그워어어어어어얽!!!!");

        m_scripts[(int)ExtractionQuestManager.Mission.Try_Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Try_Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "거대 생체반응 소멸 확인.");

        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "다 끝났소?");
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "경고. 무수한 생체반응 확인. 이쪽으로 오고있습니다.");
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][2] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "쉽게 풀리는 법이 없군. 박사, 빨리 헬기 시동을 걸어주시오, 오퍼레이터가 도와줄 것이오.");

        m_scripts[(int)ExtractionQuestManager.Mission.Hold_Until_Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Hold_Until_Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "1분만 버텨 주시오!!");

        m_scripts[(int)ExtractionQuestManager.Mission.Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "됐다!! 이봐요!! 어서 타요!!");

        m_scripts[(int)ExtractionQuestManager.Mission.END] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.END][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "구출 완료. 본부로 귀환한다.");
        m_scripts[(int)ExtractionQuestManager.Mission.END][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "헬기 즉시 송환합니다.");
    }


     private void Start()
    {
        InvokeScript();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScript()
    {
        CancelInvoke();
        m_ScriptIndex = 0;
        InvokeScript();
    }

    public void InvokeScript()
    {
        ExtractionQuestManager.Mission mission = ExtractionQuestManager.GetInstance().GetMission();
        if (m_scripts[(int)mission] == null)
        {
            return;
        }

        ScriptGroupUI scriptGroupUI = m_UIHead.GetScriptGroupUI();
        scriptGroupUI.SetImage(m_scripts[(int)mission][m_ScriptIndex].speaker, m_scripts[(int)mission][m_ScriptIndex].potrait);
        scriptGroupUI.SetText(m_scripts[(int)mission][m_ScriptIndex].speaker, m_scripts[(int)mission][m_ScriptIndex].script);
        scriptGroupUI.SetVisibility(m_scripts[(int)mission][m_ScriptIndex].speaker);

        float fTime = m_fLESSTIME + m_scripts[(int)mission][m_ScriptIndex].script.Length / 10.0f;
        if (m_scripts[(int)mission].Length <= ++m_ScriptIndex)
        {
            Invoke("DoneScript", fTime);
            m_ScriptIndex = 0;
        }
        else
        {
            Invoke("InvokeScript", fTime);
        }
    }
    private void DoneScript()
    {
        ScriptGroupUI scriptGroupUI = m_UIHead.GetScriptGroupUI();
        scriptGroupUI.SetVisibility(false);
    }
}
