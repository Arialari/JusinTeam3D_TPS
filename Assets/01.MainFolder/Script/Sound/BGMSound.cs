using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSound : MonoBehaviour
{
    private SoundScript m_SoundScript;
    private static BGMSound m_Instance;
    
    void Awake()
    {
        TryGetComponent(out m_SoundScript);
        m_Instance = this;
    }

    private void Start()
    {
        m_SoundScript.Play_LoopOn();
        m_SoundScript.Play_JustOne(0);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayNormalBGM()
    {
        m_SoundScript.m_AudioSrc.Stop();
        m_SoundScript.Play_LoopOn();
        m_SoundScript.Play_JustOne(0);
    }

    public void PlayBossBGM()
    {
        m_SoundScript.m_AudioSrc.Stop();
        m_SoundScript.Play_LoopOn();
        m_SoundScript.Play_JustOne(1);
    }

    public void PlayWaveBGM()
    {
        m_SoundScript.m_AudioSrc.Stop();
        m_SoundScript.Play_LoopOn();
        m_SoundScript.Play_JustOne(2);
        m_SoundScript.Play_JustOne(3);
    }

    public static BGMSound GetInstance()
    {
        return m_Instance;
    }

}
