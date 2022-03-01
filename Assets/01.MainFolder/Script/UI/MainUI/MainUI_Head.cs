using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI_Head : MonoBehaviour
{
    public GameObject m_TransTxt_Money;

    MainMenuUI m_MainMenuUI;

    GameObject m_InGamePanel;

    TPSCharacterController m_TPSCharacterController;

    PlayerStatus m_PlayerStat;
    HeavyGun m_HeavyGun;
    Text m_txtHeavyGunAmmo;

    private ScriptGroupUI m_ScriptGroupUI;
    private GameObject m_PlayerHpBox;
    private GameObject m_MoneyUI;
    
    private float m_fAccTime = 0f;
    private bool m_bHeavyGun = false;

    // Start is called before the first frame update
    private void Awake()
    {
        m_InGamePanel = GameObject.Find("In Game Panel");
        m_ScriptGroupUI = m_InGamePanel.transform.GetTransformInAllChildren("Script Group").GetComponent<ScriptGroupUI>();

        m_PlayerHpBox = m_InGamePanel.transform.GetTransformInAllChildren("HpBox").gameObject;
        m_MoneyUI = m_InGamePanel.transform.GetTransformInAllChildren("PocketMoneyTXT").gameObject;
    }
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        m_HeavyGun = GameObject.Find("M2_50cal").GetComponent<HeavyGun>();
        m_MainMenuUI = GameObject.Find("Main Menu Panel").GetComponent<MainMenuUI>();
        
        m_TPSCharacterController = GameObject.Find("MainCam").GetComponent<TPSCharacterController>();

        m_txtHeavyGunAmmo = m_InGamePanel.transform.GetTransformInAllChildren("HeavyGun Group").GetTransformInAllChildren("Heavy Ammo").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        m_fAccTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!m_MainMenuUI.m_bCloseMeunOn)
            {
                m_InGamePanel.SetActive(false);
                m_MainMenuUI.m_bCloseMeunOn = true;
                Cursor.lockState = CursorLockMode.None;
                m_TPSCharacterController._bOpenUI = true;
            }
            else
            {
                m_InGamePanel.SetActive(true);
                m_MainMenuUI.m_bCloseMeunOn = false;
                Cursor.lockState = CursorLockMode.Locked;
                m_TPSCharacterController._bOpenUI = false;
            }
        }
        HeavyGunUI_On();
        HeavyGunAmmo();
        Active_Window();

        PlayerHpBox_ScaleDown();
    }

    void Active_Window()
    {
        if (m_MainMenuUI.m_bCloseMeunOn) // 메인메뉴 창
        {
            m_MainMenuUI.gameObject.SetActive(true);
        }
        else
        {
            m_MainMenuUI.gameObject.SetActive(false);
        }
    }

    private void HeavyGunUI_On()
    {
        if (m_PlayerStat.GetAnimator().GetBool("HeavyGrip"))
        {
            if(!m_bHeavyGun)
            {
                m_InGamePanel.transform.GetTransformInAllChildren("Weapon Group").gameObject.SetActive(false);
                m_InGamePanel.transform.GetTransformInAllChildren("HeavyGun Group").gameObject.SetActive(true);
                m_bHeavyGun = true;
            }            
        }

        else
        {
            if (m_bHeavyGun)
            {
                m_InGamePanel.transform.GetTransformInAllChildren("HeavyGun Group").gameObject.SetActive(false);
                m_InGamePanel.transform.GetTransformInAllChildren("Weapon Group").gameObject.SetActive(true);
                m_bHeavyGun = false;
            }
        }
    }

    private void HeavyGunAmmo()
    {
        m_txtHeavyGunAmmo.text = string.Format("{0:D3}", m_HeavyGun.m_Info._iAmmo);
    }

    public void Set_MissionTitle(string _Title)
    {
        m_InGamePanel.transform.GetTransformInAllChildren("Mission_Title").GetComponent<Text>().text = _Title;
    }

    public ScriptGroupUI GetScriptGroupUI()
    {
        return m_ScriptGroupUI;
    }

    public void PlayerHpBox_ScaleUp()
    {
        if (m_PlayerHpBox.GetComponent<RectTransform>().localScale.magnitude < new Vector3(0.7f, 0.7f, 0.7f).magnitude)
            m_PlayerHpBox.GetComponent<RectTransform>().localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void PlayerHpBox_ScaleDown()
    {
        if (m_fAccTime >= 2f && m_PlayerHpBox.GetComponent<RectTransform>().localScale.magnitude > new Vector3(0.6f, 0.6f, 0.6f).magnitude)
        {
            m_PlayerHpBox.GetComponent<RectTransform>().localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    public void Add_PlayerMoneyEffect()
    {
        Instantiate(m_TransTxt_Money, m_MoneyUI.transform);
    }

    public void OnlyScriptUI()
    {
        for(int i=0; i<m_InGamePanel.transform.childCount-1; ++i)
        {
            m_InGamePanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
