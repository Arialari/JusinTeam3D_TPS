using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGroupUI : MonoBehaviour
{
    public enum Speaker { Player, Other, End }
    public ScriptUI[] m_ScriptUI = new ScriptUI[(int)Speaker.End];

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(Speaker _speaker, string _script)
    {
        m_ScriptUI[(int)_speaker].SetText(_script);
    }
    public void SetImage(Speaker speaker, Texture texture)
    {
        m_ScriptUI[(int)speaker].SetImage(texture);
    }

    public void SetVisibility(Speaker speaker)
    {
        m_ScriptUI[(int)speaker].SetVisibility(true);
        m_ScriptUI[1-(int)speaker].SetVisibility(false);
    }

    public void SetVisibility(bool bIsVisible)
    {
        m_ScriptUI[(int)Speaker.Player].SetVisibility(bIsVisible);
        m_ScriptUI[(int)Speaker.Other].SetVisibility(bIsVisible);
    }
}
