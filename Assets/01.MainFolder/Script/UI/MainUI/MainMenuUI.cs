using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Text m_txtMissionTitle;
    public Text m_txtMissionDetail;

    private Mission_Script m_MissionScript;

    public GameObject m_CloseMenuButton { get; set; }

    [HideInInspector] public bool m_bCloseMeunOn = false;

    public GameObject m_InGamePanel { get; set; }
    public GameObject m_ShopMenuPanel { get; set; }

    public Shop_Menu m_Shop_Menu { get; set; }
    public Btn_Shop_Weap01 m_Btn_ShopWeap01 { get; set; }
    public Btn_Shop_Weap02 m_Btn_ShopWeap02 { get; set; }
    public TPSCharacterController m_TPSCharacterController { get; set; }

    public MainUI_Head m_MainUI_Head { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        m_MainUI_Head = GetComponentInParent<MainUI_Head>();
        m_MissionScript = GetComponent<Mission_Script>();
        m_CloseMenuButton = GameObject.Find("Close Button");

        m_InGamePanel = GameObject.Find("In Game Panel");
        m_ShopMenuPanel = GameObject.Find("Shop UI Panel 1");

        m_Shop_Menu = m_ShopMenuPanel.GetComponent<Shop_Menu>();

        m_Btn_ShopWeap01 = m_ShopMenuPanel.transform.GetTransformInAllChildren("Btn_Weap01").GetComponent<Btn_Shop_Weap01>();
        m_Btn_ShopWeap02 = m_ShopMenuPanel.transform.GetTransformInAllChildren("Btn_Weap02").GetComponent<Btn_Shop_Weap02>();

        m_TPSCharacterController = GameObject.Find("MainCam").GetComponent<TPSCharacterController>();
    }

    // Update is called once per frame
    public void Update()
    {
        Active_Menu();
        Change_Mission();
    }

    private void Active_Menu()
    {
        if (m_Shop_Menu._bCloseToShopMenu)
            m_ShopMenuPanel.SetActive(true);
        else
        {
            m_Btn_ShopWeap01.RandomSetWeapon();
            m_Btn_ShopWeap02.RandomSetWeapon();
            m_ShopMenuPanel.SetActive(false);
        }
    }

    public void ClickToCloseMenu()
    {
        m_InGamePanel.SetActive(true);
        m_bCloseMeunOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_TPSCharacterController._bOpenUI = false;
    }

    public void OpenToShop()
    {
        if (!m_Shop_Menu._bCloseToShopMenu)
        {
            m_InGamePanel.SetActive(false);
            m_Shop_Menu._bCloseToShopMenu = true;
            Cursor.lockState = CursorLockMode.None;
            m_TPSCharacterController._bOpenUI = true;
        }
        else
        {
            m_InGamePanel.SetActive(true);
            m_Shop_Menu._bCloseToShopMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
            m_TPSCharacterController._bOpenUI = false;
        }
    }

    public void Change_Mission()
    {
        m_txtMissionTitle.text = m_MissionScript.m_MissionInfo[ExtractionQuestManager.GetInstance().GetMission()]._strTitle;
        m_MainUI_Head.Set_MissionTitle(m_MissionScript.m_MissionInfo[ExtractionQuestManager.GetInstance().GetMission()]._strTitle);
        m_txtMissionDetail.text = m_MissionScript.m_MissionInfo[ExtractionQuestManager.GetInstance().GetMission()]._strDetail;
        return;
    }
}
