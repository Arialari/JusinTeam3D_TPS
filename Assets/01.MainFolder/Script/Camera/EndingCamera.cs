using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCamera : MonoBehaviour
{
    public Transform m_HeliTransform;
    private AudioListener m_AudioListener;
    private Camera m_Camera;
    public Camera m_MainCamera;
    private MainUI_Head m_UI;
    void Start()
    {
        TryGetComponent(out m_AudioListener);
        TryGetComponent(out m_Camera);
        GameObject.Find("Canvas").TryGetComponent(out m_UI);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_HeliTransform);
    }

    public void Active()
    {
        m_UI.OnlyScriptUI();
        m_MainCamera.enabled = false;
        m_MainCamera.GetComponent<AudioListener>().enabled = false;
        m_Camera.enabled = true;
        m_AudioListener.enabled = true;
    }
}
