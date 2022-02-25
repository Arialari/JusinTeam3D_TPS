using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreacherAnimReceiver : MonoBehaviour
{
    private BreacherAI m_BreacherAI;
    private AIViewPerception m_AIViewPerception;
    private Animator m_Animator;
    private EnemyStatus m_EnemyStatus;
    private SoundScript m_SoundScript;
    void Awake()
    {
        TryGetComponent(out m_AIViewPerception);
        TryGetComponent(out m_BreacherAI);
        TryGetComponent(out m_Animator);
        TryGetComponent(out m_EnemyStatus);
        TryGetComponent(out m_SoundScript);
    }
    public void EndScream()
    {
        int _ClipNum = Random.Range(3, 4);
        m_BreacherAI.m_Soundscript.Play_JustOne(_ClipNum);
        m_AIViewPerception.m_eAlert = AIViewPerception.Alert.Alert;
        m_Animator.SetBool("IsAlert", true);
        m_BreacherAI.m_bIsMoveable = true;
    }
    public void StartAttack()
    {
        int _ClipNum = Random.Range(5, 7);
        m_BreacherAI.m_Soundscript.Play_JustOne(_ClipNum);
        m_BreacherAI.m_bIsMoveable = false;
    }
    public void EndAttack()
    {
        m_BreacherAI.m_bReadyToAttack = true;
        m_BreacherAI.m_bIsMoveable = true;
    }

    public void EndDead()
    {
        m_BreacherAI.Die();
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
        m_BreacherAI.m_bIsMoveable = true;
        m_BreacherAI.TeleportToMeshLinkEnd();
    }
    private void StartDying()
    {
        m_BreacherAI.m_bIsMoveable = false;
    }

    public void Attack_Ready()
    {
        
    }
}
