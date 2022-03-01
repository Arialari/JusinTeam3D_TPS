using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Exit_Script : MonoBehaviour
{
    public Lobby_UI_Script m_LobbyUIscript;

    public Button m_Btn_Accept;
    public Button m_Btn_Cancle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTo_Accept()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClickTo_Cancle()
    {
        m_LobbyUIscript.m_Anim_btn_Exit.SetBool("Selected", false);
        this.gameObject.SetActive(false);
    }
}
