using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI_Script : MonoBehaviour
{
    public Canvas m_LobbyCanvas;

    public GameObject m_PlayerCharacter;
    private Transform m_PlayerCharacterTrans;

    private GameObject m_PanelCopy;
    private GameObject m_CopyOBJ;

    private Panel_Dictionary_Script m_Dictionary_Script;

    public GameObject m_Panel_Lobby;
    public GameObject m_Panel_Custom;
    public GameObject m_Panel_Dictionary;
    public GameObject m_Panel_Credit;
    public GameObject m_Panel_Option;
    public GameObject m_Panel_Exit;

    public Button m_Btn_Lobby;
    public Button m_Btn_Custom;
    public Button m_Btn_Dictionary;
    public Button m_Btn_Credit;
    public Button m_Btn_Option;
    public Button m_Btn_Exit;

    [HideInInspector] public Animator m_Anim_btn_Lobby;
    [HideInInspector] public Animator m_Anim_btn_Custom;
    [HideInInspector] public Animator m_Anim_btn_Dictionary;
    [HideInInspector] public Animator m_Anim_btn_Credit;
    [HideInInspector] public Animator m_Anim_btn_Option;
    [HideInInspector] public Animator m_Anim_btn_Exit;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCharacter = GameObject.Find("Lobby_Player");
        m_PlayerCharacterTrans = m_PlayerCharacter.transform;

        m_PanelCopy = m_Panel_Lobby;
        m_Panel_Lobby.SetActive(true);
        m_Panel_Custom.SetActive(false);
        m_Panel_Dictionary.SetActive(false);
        m_Panel_Credit.SetActive(false);
        m_Panel_Option.SetActive(false);
        m_Panel_Exit.SetActive(false);

        m_Anim_btn_Lobby = m_Btn_Lobby.GetComponent<Animator>();
        m_Anim_btn_Custom = m_Btn_Custom.GetComponent<Animator>();
        m_Anim_btn_Dictionary = m_Btn_Dictionary.GetComponent<Animator>();
        m_Anim_btn_Credit = m_Btn_Credit.GetComponent<Animator>();
        m_Anim_btn_Option = m_Btn_Option.GetComponent<Animator>();
        m_Anim_btn_Exit = m_Btn_Exit.GetComponent<Animator>();

        m_Dictionary_Script = m_Panel_Dictionary.GetComponent<Panel_Dictionary_Script>();

        m_Anim_btn_Lobby.SetBool("Selected", true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_Panel_Lobby.activeSelf || m_Panel_Dictionary.activeSelf) &&
            !m_Panel_Exit.activeSelf)
        {
            Rotate_OBJ();
        }
    }

    private void Rotate_OBJ()
    {
        if (m_Panel_Lobby.activeSelf)
        {
            m_CopyOBJ = m_PlayerCharacter;
        }

        if (m_Panel_Dictionary.activeSelf)
        {
        }
            

        if (Input.GetMouseButton(1))
        {
            m_CopyOBJ.transform.Rotate(m_CopyOBJ.transform.up * -5f * Input.GetAxis("Mouse X"));
        }

        if (Input.GetMouseButtonUp(1))
        {
            m_CopyOBJ.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void ClickTo_Btn_Lobby()
    {
        if (!m_Panel_Lobby.activeSelf)
            m_Panel_Lobby.SetActive(true);

        This_Btn_Active(m_Anim_btn_Lobby);
        Panel_Change(m_Anim_btn_Lobby, m_Panel_Lobby);
    }

    public void ClickTo_Btn_Custom()
    {
        if(!m_Panel_Custom.activeSelf)
            m_Panel_Custom.SetActive(true);

        This_Btn_Active(m_Anim_btn_Custom);
        Panel_Change(m_Anim_btn_Custom, m_Panel_Custom);
    }

    public void ClickTo_Btn_Dictionary()
    {
        if (!m_Panel_Dictionary.activeSelf)
            m_Panel_Dictionary.SetActive(true);
        
        m_Dictionary_Script.m_Anim_Btn_EnemyInfo.SetBool("bSelected", true);
        This_Btn_Active(m_Anim_btn_Dictionary);
        Panel_Change(m_Anim_btn_Dictionary, m_Panel_Dictionary);
    }

    public void ClickTo_Btn_Credit()
    {
        if (!m_Panel_Credit.activeSelf)
            m_Panel_Credit.SetActive(true);

        This_Btn_Active(m_Anim_btn_Credit);
        Panel_Change(m_Anim_btn_Credit, m_Panel_Credit);
    }

    public void ClickTo_Btn_Option()
    {
        if (!m_Panel_Lobby.activeSelf)
            m_Panel_Lobby.SetActive(true);

        This_Btn_Active(m_Anim_btn_Option);
        Panel_Change(m_Anim_btn_Option, m_Panel_Option);
    }

    public void ClickTo_Btn_Exit()
    {
        if (!m_Panel_Exit.activeSelf)
            m_Panel_Exit.SetActive(true);
    }

    private void This_Btn_Active(Animator _BtnAnim)
    {
        if (_BtnAnim != m_Anim_btn_Lobby)
            m_Anim_btn_Lobby.SetBool("Selected", false);
        else
            m_Anim_btn_Lobby.SetBool("Selected", true);

        //if (_BtnAnim != m_Anim_btn_Custom)
        //    m_Anim_btn_Custom.SetBool("Selected", false);
        //else
        //    m_Anim_btn_Custom.SetBool("Selected", true);

        if (_BtnAnim != m_Anim_btn_Dictionary)
            m_Anim_btn_Dictionary.SetBool("Selected", false);
        else
            m_Anim_btn_Dictionary.SetBool("Selected", true);

        if (_BtnAnim != m_Anim_btn_Credit)
            m_Anim_btn_Credit.SetBool("Selected", false);
        else
            m_Anim_btn_Credit.SetBool("Selected", true);

        if (_BtnAnim != m_Anim_btn_Option)
            m_Anim_btn_Option.SetBool("Selected", false);
        else
            m_Anim_btn_Option.SetBool("Selected", true);
    }

    private void Panel_Change(Animator _AnimName, GameObject _PanelName)
    {
        if(m_PanelCopy)
            m_PanelCopy.SetActive(false);

        if (_AnimName.GetBool("Selected"))
        {
            _PanelName.SetActive(true);
        }
        
        m_PanelCopy = _PanelName;
    }
}
