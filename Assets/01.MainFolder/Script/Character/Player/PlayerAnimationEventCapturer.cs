using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCapturer : MonoBehaviour
{
    private PlayerStatus m_PlayerStatus;
    private Animator m_Animator;
   void Start()
    {
        TryGetComponent(out m_Animator);
        m_PlayerStatus = GetComponentInParent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndShootingAnimation()
    {
    }

    public void Throw_Bomb()
    {
        m_PlayerStatus.ThrowBOMB();
    }

    public void Use_Medicine()
    {
        m_PlayerStatus.UseMedicine();
    }

    public void SlashEnd()
    {
        m_PlayerStatus.SlashEnd();
    }

    public void Reloaded()
    {
        m_PlayerStatus.Reloaded();
        m_PlayerStatus.SetAnimatorParameterBool("Reload", false);
        m_PlayerStatus.SetAnimatorParameterBool("Aiming", false);
    }

    public void ShotEnd()
    {
        m_PlayerStatus.m_bShot = false;
    }

    public void MeleeAttack()
    {
        m_PlayerStatus.MeleeAttack();
    }

    public void Player_Walk()
    {
        int _iClip = Random.Range(1, 4);
        m_PlayerStatus.m_SoundScript.Play_JustOne(_iClip);
    }

    public void Player_Run()
    {
        int _iClip = Random.Range(1, 4);
        m_PlayerStatus.m_SoundScript.Play_JustOne(_iClip);
    }
}
