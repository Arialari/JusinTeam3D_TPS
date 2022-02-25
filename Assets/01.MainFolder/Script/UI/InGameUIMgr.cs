using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIMgr : MonoBehaviour
{
    [HideInInspector] public GameObject m_Cam_InGame;

    [HideInInspector] public GameObject m_Player;
    private PlayerStatus m_PlayerStat;

    private GameObject m_InGamePanel;
    [HideInInspector] public GameObject m_InteractionUI;
    [HideInInspector] public Text m_InteractionUI_TEXT;

    // UI영역
    #region UI
    [HideInInspector] public GameObject m_Panel_Menu;
    [HideInInspector] public GameObject m_Panel_InGame;

    // 주무기
    #region Tab_MainWeap
    [HideInInspector] public GameObject m_Tab_MainWeap;
    private RectTransform m_Tab_MainWeap_RectTransform;
    private CanvasGroup m_Group_MainWeap;
    [HideInInspector] public Text m_Ammo_MainWeap;
    [HideInInspector] public Text m_MaxAmmo_MainWeap;
    [HideInInspector] public Image m_PrimaryAmmoIcon;
    bool bMainWeap;
    bool bForceChangeThrowToMainWeap;
    bool bForceChangeHealToMainWeap;
    #endregion

    // 보조무기
    #region Tab_SubWeap
    [HideInInspector] public GameObject m_Tab_SubWeap;
    private RectTransform m_Tab_SubWeap_RectTransform;
    private CanvasGroup m_Group_SubWeap;
    [HideInInspector] public Text m_Ammo_SubWeap;
    [HideInInspector] public Text m_MaxAmmo_SubWeap;
    [HideInInspector] public Image m_SecondaryAmmoIcon;
    [HideInInspector] public Image m_InfinityIcon;
    bool bSubWeap;
    #endregion

    // 투척아이템
    #region Tab_Throwable
    [HideInInspector] public GameObject m_Tab_Throwable;
    private RectTransform m_Tab_Throwable_RectTransform;
    private CanvasGroup m_Group_Throwable;
    [HideInInspector] public Text m_Ammo_Throwable;
    bool bThrowable;
    #endregion

    // 치료아이템
    #region Tab_HealingItem
    [HideInInspector] public GameObject m_Tab_HealingItem;
    private RectTransform m_Tab_HealingItem_RectTransform;
    private CanvasGroup m_Group_HealingItem;
    [HideInInspector] public Text m_Ammo_HealingItem;
    bool bHealingItem;
    #endregion

    #region PlayerINFO
    GameObject _PlayerINFO;
    GameObject _HpBar;
    GameObject _PlayerFace;
    GameObject _TemporaryHP_UpperBar;
    GameObject _TemporaryHP_UnderBar;
    GameObject _TotalHpText;

    Text m_txtPocketMoney;
    #endregion

    [HideInInspector] public Image m_TabImg_PrimaryWeap;
    [HideInInspector] public Image m_TabImg_SecondaryWeap;
    [HideInInspector] public Image m_TabImg_Throwalble;
    [HideInInspector] public Image m_TabImg_Medicine;
    #endregion

    public Using_GageBar m_Using_GageBar;

    // Start is called before the first frame update
    void Start()
    {
        m_Cam_InGame = GameObject.Find("Camera");
        m_Player = GameObject.Find("Player");
        m_Panel_Menu = GameObject.Find("Main Menu Panel");
        m_Panel_InGame = GameObject.Find("In Game Panel");
        m_Tab_MainWeap = GameObject.Find("Tab_PrimaryWeap");
        m_Tab_SubWeap = GameObject.Find("Tab_SecondaryWeap");
        m_Tab_Throwable = GameObject.Find("Tab_ThrowAble");
        m_Tab_HealingItem = GameObject.Find("Tab_HealingItem");
        
        m_PlayerStat = m_Player.GetComponent<PlayerStatus>();
        m_Group_MainWeap = m_Tab_MainWeap.GetComponent<CanvasGroup>();
        m_Tab_MainWeap_RectTransform = m_Tab_MainWeap.GetComponent<RectTransform>();
        m_Group_SubWeap = m_Tab_SubWeap.GetComponent<CanvasGroup>();
        m_Tab_SubWeap_RectTransform = m_Tab_SubWeap.GetComponent<RectTransform>();
        m_Group_Throwable = m_Tab_Throwable.GetComponent<CanvasGroup>();
        m_Tab_Throwable_RectTransform = m_Tab_Throwable.GetComponent<RectTransform>();
        m_Group_HealingItem = m_Tab_HealingItem.GetComponent<CanvasGroup>();
        m_Tab_HealingItem_RectTransform = m_Tab_HealingItem.GetComponent<RectTransform>();

        _HpBar = GameObject.Find("Canvas").transform.GetTransformInAllChildren("HpBar").gameObject;
        _TemporaryHP_UnderBar = GameObject.Find("Canvas").transform.GetTransformInAllChildren("Temporary HpBar1").gameObject;
        _TemporaryHP_UpperBar = GameObject.Find("Canvas").transform.GetTransformInAllChildren("Temporary HpBar2").gameObject;
        _TotalHpText = GameObject.Find("Canvas").transform.GetTransformInAllChildren("Text (1)").gameObject;
        m_InGamePanel = GameObject.Find("In Game Panel");
        m_InteractionUI = m_InGamePanel.transform.GetTransformInAllChildren("Interaction").gameObject;
        m_InteractionUI_TEXT = m_InGamePanel.transform.GetTransformInAllChildren("ChangeText").GetComponent<Text>();

        m_Ammo_MainWeap = m_InGamePanel.transform.GetTransformInAllChildren("Primary Ammo").GetComponent<Text>();
        m_MaxAmmo_MainWeap = m_InGamePanel.transform.GetTransformInAllChildren("Primary MaxAmmo").GetComponent<Text>();
        m_Ammo_SubWeap = m_InGamePanel.transform.GetTransformInAllChildren("Secondary Ammo").GetComponent<Text>();
        m_MaxAmmo_SubWeap = m_InGamePanel.transform.GetTransformInAllChildren("Secondary MaxAmmo").GetComponent<Text>();

        m_txtPocketMoney = GameObject.Find("PocketMoneyTXT").GetComponent<Text>();
        m_TabImg_PrimaryWeap = m_InGamePanel.transform.GetTransformInAllChildren("PrimaryWeapImg").GetComponent<Image>();
        m_TabImg_SecondaryWeap = m_InGamePanel.transform.GetTransformInAllChildren("SecondaryWeapImg").GetComponent<Image>();

        m_PrimaryAmmoIcon = m_InGamePanel.transform.GetTransformInAllChildren("PrimaryBullet_Img").GetComponent<Image>();
        m_SecondaryAmmoIcon = m_InGamePanel.transform.GetTransformInAllChildren("SecondaryBullet_Img").GetComponent<Image>();
        m_TabImg_Medicine = m_InGamePanel.transform.GetTransformInAllChildren("HealingItem Img").GetComponent<Image>();
        m_TabImg_Throwalble = m_InGamePanel.transform.GetTransformInAllChildren("Throwable Img").GetComponent<Image>();

        m_Ammo_Throwable = m_InGamePanel.transform.GetTransformInAllChildren("Throwable Ammo").GetComponent<Text>();
        m_Ammo_HealingItem = m_InGamePanel.transform.GetTransformInAllChildren("HealingItem Ammo").GetComponent<Text>();

        m_InfinityIcon = GameObject.Find("Infinity Icon").GetComponent<Image>();

        m_Using_GageBar = m_InGamePanel.transform.GetTransformInAllChildren("Using_GageBar").GetComponent<Using_GageBar>();

        m_InfinityIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwapWeap();
        Update_HpBar();
    }

    void LateUpdate()
    {
        m_txtPocketMoney.text = string.Format("{0:n0}", m_PlayerStat.m_iPocketMoney);

        m_Ammo_MainWeap.text = string.Format("{0:D2}", m_PlayerStat.GetPrimaryWeapon().GetAmmo());
        m_MaxAmmo_MainWeap.text = string.Format("{0:D3}", m_PlayerStat.GetPrimaryWeapon().GetTotalAmmo());

        m_Ammo_SubWeap.text = string.Format("{0:D2}", m_PlayerStat.GetSecondaryWeapon().GetAmmo());
        m_MaxAmmo_SubWeap.text = string.Format("{0:D3}", m_PlayerStat.GetSecondaryWeapon().GetTotalAmmo());

        m_Ammo_Throwable.text = string.Format("{0:n0}", m_PlayerStat.m_iAmmo_Throwable);
        m_Ammo_HealingItem.text = string.Format("{0:n0}", m_PlayerStat.m_iAmmo_Medicine);
    }

    void SwapWeap()
    {
        switch(m_PlayerStat.GetSelectedItem())
        {
            case PlayerStatus.Item.Primary:
                m_Tab_MainWeap_RectTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                m_Group_MainWeap.alpha = 1.0f;

                m_Tab_SubWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_SubWeap.alpha = 0.5f;

                m_Tab_Throwable_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_Throwable.alpha = 0.5f;

                m_Tab_HealingItem_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_HealingItem.alpha = 0.5f;
                break;
            case PlayerStatus.Item.Secondary:
                m_Tab_MainWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_MainWeap.alpha = 0.5f;

                m_Tab_SubWeap_RectTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                m_Group_SubWeap.alpha = 1.0f;

                m_Tab_Throwable_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_Throwable.alpha = 0.5f;

                m_Tab_HealingItem_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_HealingItem.alpha = 0.5f;
                break;
            case PlayerStatus.Item.Throwing:
                m_Tab_MainWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_MainWeap.alpha = 0.5f;

                m_Tab_SubWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_SubWeap.alpha = 0.5f;

                m_Tab_Throwable_RectTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                m_Group_Throwable.alpha = 1.0f;

                m_Tab_HealingItem_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_HealingItem.alpha = 0.5f;
                break;
            case PlayerStatus.Item.Healing:
                m_Tab_MainWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_MainWeap.alpha = 0.5f;

                m_Tab_SubWeap_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_SubWeap.alpha = 0.5f;

                m_Tab_Throwable_RectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                m_Group_Throwable.alpha = 0.5f;

                m_Tab_HealingItem_RectTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                m_Group_HealingItem.alpha = 1.0f;
                break;
        }
    }

    void Update_HpBar()
    {
        _HpBar.GetComponent<Image>().fillAmount = 1.0f / m_PlayerStat._MaxHP * m_PlayerStat._HP;
        _TemporaryHP_UnderBar.GetComponent<Image>().fillAmount = 1.0f / m_PlayerStat._MaxHP * m_PlayerStat._TemporaryHP_Under;
        _TemporaryHP_UpperBar.GetComponent<Image>().fillAmount = 1.0f / m_PlayerStat._MaxHP * m_PlayerStat._TemporaryHP_Upper;

        if (m_PlayerStat._HP <= 50 && m_PlayerStat._HP > 20)
            _HpBar.GetComponent<Image>().color = new Color(1, 0.7f, 0);
        else if (m_PlayerStat._HP <= 20)
            _HpBar.GetComponent<Image>().color = new Color(1, 0.3f, 0.3f);
        else
            _HpBar.GetComponent<Image>().color = new Color(1, 1, 1);

        _TotalHpText.GetComponent<Text>().text = string.Format("{0:n0}", m_PlayerStat._TemporaryHP_Under + m_PlayerStat._TemporaryHP_Upper);

        if (m_PlayerStat._TemporaryHP_UnderON)
        {
            if (m_PlayerStat._TemporaryHP_UpperON)
            {
                _TotalHpText.GetComponent<Text>().color = _TemporaryHP_UpperBar.GetComponent<Image>().color;
                return;
            }
            _TotalHpText.GetComponent<Text>().color = _TemporaryHP_UnderBar.GetComponent<Image>().color;
        }
        
        else if (m_PlayerStat._TemporaryHP_UpperON &&
            !m_PlayerStat._TemporaryHP_UnderON)
            _TotalHpText.GetComponent<Text>().color = _TemporaryHP_UpperBar.GetComponent<Image>().color;

        else if (m_PlayerStat._HP <= 50 && m_PlayerStat._HP > 20)
            _TotalHpText.GetComponent<Text>().color = new Color(1, 0.7f, 0);

        else if (m_PlayerStat._HP <= 20)
            _TotalHpText.GetComponent<Text>().color = new Color(1, 0.3f, 0.3f);

        else
            _TotalHpText.GetComponent<Text>().color = Color.white;
    }
}
