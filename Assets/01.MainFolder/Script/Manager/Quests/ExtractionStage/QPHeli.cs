using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class QPHeli : MonoBehaviour, IInteractE
{
    private Collider m_collider;
    private Animation m_Animation;
    private GameObject m_Player;
    private bool m_bActive = false;
    private float m_fSpeed = 10.0f;
    public EndingCamera m_EndingCamrea;
    public SoundScript m_SoundScript;

    private int m_iCount = 1;

    void Awake()
    {
        TryGetComponent(out m_Animation);
        TryGetComponent(out m_collider);
        m_Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bActive)
        {
            Sound();

            if (transform.position.y < 60.0f)
                transform.position += transform.up * Time.deltaTime * m_fSpeed;
            else
                transform.position += transform.forward * Time.deltaTime * m_fSpeed;            
        }

        if(transform.position.x < 250f)
        {
            SceneManager.LoadScene("SYT_LOBBY_Scene");
        }    
    }

    public void InteractE()
    {
        ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Extract);
        m_EndingCamrea.Active();
        m_bActive = true;
        m_Player.GetComponent<NavMeshAgent>().Warp(new Vector3(0, 0, 0));
    }

    public void ActiveCollider()
    {
        m_Animation.Play();
        m_collider.enabled = true;
    }

    private void Sound()
    {
        if (m_iCount == 1)
        {
            m_SoundScript.Play_JustOne(0);
            m_SoundScript.Play_LoopOn();
            m_SoundScript.m_AudioSrc.Play(1);
            m_iCount = 0;
        }
    }
}
