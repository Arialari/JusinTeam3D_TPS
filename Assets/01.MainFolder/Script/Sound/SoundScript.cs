using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource m_AudioSrc;
    public AudioClip[] m_Audioclip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_JustOne(int _clipNum)
    {
        m_AudioSrc.PlayOneShot(m_Audioclip[_clipNum]);
    }

    public void Play_LoopOn()
    {
        m_AudioSrc.loop = true;
    }

    public void Play_LoopOff()
    {
        m_AudioSrc.loop = false;
    }
}
