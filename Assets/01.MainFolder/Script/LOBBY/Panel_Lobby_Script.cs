using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_Lobby_Script : MonoBehaviour
{
    public Lobby_UI_Script m_UIScript;

    public Button m_Btn_GameStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTo_GameStart()
    {
        SceneManager.LoadScene("SYT_ExtractionStage");
    }
}
