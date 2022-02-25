using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Menu : MonoBehaviour
{
    public bool _bCloseToShopMenu;

    public bool _bOn_ItemInfo = false;
    public bool _bOn_WeapInfo = false;

    TPSCharacterController _TPSCharacterController;

    #region ### PANELS ###

    #region ### Info Panel ###
    public GameObject m_InfoPanel;
    public Image m_Info_ItemImage;
    public Image m_Info_WeapImage;

    public Text m_txtName;
    public Text m_txtKind;
    public Text m_txtCharTitle;
    public Text m_txtChar;
    public Text m_txtExplane;
    public Text m_txtPrice01;
    public Text m_txtPrice02;
    #endregion

    #endregion

    GameObject m_InGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        _bCloseToShopMenu = false;
        _TPSCharacterController = GameObject.Find("MainCam").GetComponent<TPSCharacterController>();
        m_InGamePanel = GameObject.Find("In Game Panel");

        m_InfoPanel = this.transform.GetTransformInAllChildren("Info_Panel").gameObject;
        m_Info_ItemImage = m_InfoPanel.transform.GetTransformInAllChildren("BackGround").transform.GetTransformInAllChildren("Info_ItemImage").GetComponent<Image>();
        m_Info_WeapImage = m_InfoPanel.transform.GetTransformInAllChildren("BackGround").transform.GetTransformInAllChildren("Info_WeapImage").GetComponent<Image>();

        m_txtName = m_InfoPanel.transform.GetTransformInAllChildren("ItemName").GetComponent<Text>();
        m_txtKind = m_InfoPanel.transform.GetTransformInAllChildren("ItemKind").GetComponent<Text>();
        m_txtCharTitle = m_InfoPanel.transform.GetTransformInAllChildren("ItemCharTitle").GetComponent<Text>();
        m_txtChar = m_InfoPanel.transform.GetTransformInAllChildren("ItemChar").GetComponent<Text>();
        m_txtExplane = m_InfoPanel.transform.GetTransformInAllChildren("ItemExplane").GetComponent<Text>();
        m_txtPrice01 = m_InfoPanel.transform.parent.GetTransformInAllChildren("Price01").GetComponent<Text>();
        m_txtPrice02 = m_InfoPanel.transform.parent.GetTransformInAllChildren("Price02").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        View_Info();
    }

    public void CloseToShopMenu()
    {
        m_InGamePanel.SetActive(true);
        _bCloseToShopMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        _TPSCharacterController._bOpenUI = false;
    }

    private void View_Info()
    {
        
        if (_bOn_ItemInfo)
        {
            m_InfoPanel.SetActive(true);

            if (!_bOn_WeapInfo)
            {
                m_Info_WeapImage.gameObject.SetActive(false);
                m_Info_ItemImage.gameObject.SetActive(true);
            }

            else
            {
                m_Info_WeapImage.gameObject.SetActive(true);
                m_Info_ItemImage.gameObject.SetActive(false);
            }
        }

        else
        { 
            m_InfoPanel.SetActive(false);
        }
    }
}
