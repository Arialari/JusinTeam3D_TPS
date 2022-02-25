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
            m_tPlayer, "��ü��ȣ 118-A ��ǥ���� �����߽��ϴ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Door][1] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "���Ϸ� ���� ���ɻ��·� ���� �Ǿ����ϴ�. �ӹ��� �����մϴ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop][0] = new Script(ScriptGroupUI.Speaker.Player,
            m_tPlayer, "��屺");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Laptop][1] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "���� ���� ����ֽ��ϴ�. ���� �ܼ� ��ǥ�� �����Ͽ����ϴ�. �ܼ� ��ġ�� �̵��Ͽ� �ܼ��� ���� �Ͻʽÿ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Middle] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Middle][0] = new Script(ScriptGroupUI.Speaker.Other,
            m_tOperator, "���� ���� Ȯ��. �������� ���� �̵� �����մϴ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Open_The_Door2] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Open_The_Door2][0] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "�ǹ� ���� ��ĵ ��..");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Defence] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Defence][0] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "���� ��ǥ �ĺ� �Ϸ�. ��ǥ�� �����մϴ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Defence] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "�ȿ� ��ʴϱ�? �������Դϴ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][1] = new Script(ScriptGroupUI.Speaker.Other,
           m_tNPC, "����! �����밡..!! ���̽ÿ� ���� �����մϴ�!!");
        m_scripts[(int)ExtractionQuestManager.Mission.Defence][2] = new Script(ScriptGroupUI.Speaker.Other,
           m_tOperator, "���� ��ǥ�� ������ ���� �ֺ� ���� ����� �����Ͻʽÿ�");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "�̰ɷ� ���ΰ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_Hastage][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "���� ������ Ȯ��. ���� ��ǥ�� Ȯ���Ͻʽÿ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "�Ŀ�.. ���� �� �������� ������ ������. �� ���� ��� �����ڷᰡ �ּ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][1] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "�̷�.. ���� �þ���. ���۷�����?");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_ExitDoor][2] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "�������� ��ǥ�� �����մϴ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "�˷����� ���� �Ŵ� ��ü���� Ȯ��. Ż���Ʈ�� Ȯ���Ͻʽÿ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][1] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "�ڻ�� ���������� ���ÿ�. �ڷḦ �� ì����� ���� ���� �������� ������ ���ÿ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Get_To_Building][2] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "��.. �˰ڼ�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "�̸���. ���� �پ�ڰ�");
        m_scripts[(int)ExtractionQuestManager.Mission.Boss_Fight][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tBoss, "�׿��������!!!!");

        m_scripts[(int)ExtractionQuestManager.Mission.Try_Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Try_Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "�Ŵ� ��ü���� �Ҹ� Ȯ��.");

        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun] = new Script[3];
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "�� ������?");
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "���. ������ ��ü���� Ȯ��. �������� �����ֽ��ϴ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.Use_MiniGun][2] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "���� Ǯ���� ���� ����. �ڻ�, ���� ��� �õ��� �ɾ��ֽÿ�, ���۷����Ͱ� ������ ���̿�.");

        m_scripts[(int)ExtractionQuestManager.Mission.Hold_Until_Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Hold_Until_Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "1�и� ���� �ֽÿ�!!");

        m_scripts[(int)ExtractionQuestManager.Mission.Extract] = new Script[1];
        m_scripts[(int)ExtractionQuestManager.Mission.Extract][0] = new Script(ScriptGroupUI.Speaker.Other,
          m_tNPC, "�ƴ�!! �̺���!! � Ÿ��!!");

        m_scripts[(int)ExtractionQuestManager.Mission.END] = new Script[2];
        m_scripts[(int)ExtractionQuestManager.Mission.END][0] = new Script(ScriptGroupUI.Speaker.Player,
          m_tPlayer, "���� �Ϸ�. ���η� ��ȯ�Ѵ�.");
        m_scripts[(int)ExtractionQuestManager.Mission.END][1] = new Script(ScriptGroupUI.Speaker.Other,
          m_tOperator, "��� ��� ��ȯ�մϴ�.");
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
