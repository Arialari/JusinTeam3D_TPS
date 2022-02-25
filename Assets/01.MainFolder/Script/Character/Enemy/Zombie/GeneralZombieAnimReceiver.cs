using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralZombieAnimReceiver : MonoBehaviour
{
    private GeneralZombieAI m_generalZombieAI;
    private AIViewPerception m_AIViewPerception;
    private Animator m_Animator;
    private EnemyStatus m_EnemyStatus;
    private SoundScript m_Soundscript;
    void Awake()
    {
        TryGetComponent(out m_AIViewPerception);
        TryGetComponent(out m_generalZombieAI);
        TryGetComponent(out m_Animator);
        TryGetComponent(out m_EnemyStatus);
        TryGetComponent(out m_Soundscript);
    }
    public void EndScream()
    {
        m_AIViewPerception.m_eAlert = AIViewPerception.Alert.Alert;
        m_Animator.SetBool("IsAlert", true);
        m_generalZombieAI.m_bIsMoveable = true;
    }
    public void StartAttack()
    {
        int _ClipNum = Random.Range(0, 3);
        m_Soundscript.Play_JustOne(_ClipNum);
        m_generalZombieAI.m_bIsMoveable = false;
    }
    public void EndAttack()
    {
        m_generalZombieAI.m_bReadyToAttack = true;
        m_generalZombieAI.m_bIsMoveable = true;
    }

    public void EndDead()
    {
        m_generalZombieAI.Die();
    }

    public void StartAttackTrigger()
    {
        m_EnemyStatus.m_bIsAttacking = true;
    }

    public void EndAttackTrigger()
    {
        m_EnemyStatus.m_bIsAttacking = false;
    }

    public void EndVaultOver()
    {
        m_generalZombieAI.m_bIsMoveable = true;
        m_generalZombieAI.TeleportToMeshLinkEnd();
    }
    public void StartDying()
    {
        int _ClipNum = Random.Range(6, 7);
        m_Soundscript.Play_JustOne(_ClipNum);
        m_generalZombieAI.m_bIsMoveable = false;
    }

    public void Screaming()
    {
        m_Soundscript.Play_JustOne(8);
    }
}
