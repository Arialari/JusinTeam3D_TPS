using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooterAnimReceiver : MonoBehaviour
{
    private RooterAI m_RooterAI;
    private AIViewPerception m_AIViewPerception;
    private Animator m_Animator;
    private EnemyStatus m_EnemyStatus;
    void Awake()
    {
        TryGetComponent(out m_AIViewPerception);
        TryGetComponent(out m_RooterAI);
        TryGetComponent(out m_Animator);
        TryGetComponent(out m_EnemyStatus);
    }
    public void EndScream()
    {
        m_AIViewPerception.m_eAlert = AIViewPerception.Alert.Alert;
        m_Animator.SetBool("IsAlert", true);
        m_RooterAI.m_bIsMoveable = true;
        m_RooterAI.SetAlert(true);
    }
    public void EndAttack()
    {
        m_RooterAI.m_bReadyToAttack = true;
        m_RooterAI.m_bIsMoveable = true;
    }

    public void EndDead()
    {
        m_RooterAI.Die();
    }

    public void AttackTrigger()
    {
        m_RooterAI.m_Soundscript.Play_JustOne(2);
        m_EnemyStatus.m_bIsAttacking = true;
        m_RooterAI.Attack();
    }

    private void EndTeleport()
    {
        m_RooterAI.EndTeleport();
    }
    private void Teleport()
    {
        m_RooterAI.StartTeleport();
    }

    private void StartDying()
    {
        m_RooterAI.m_bIsMoveable = false;
    }
    private void EndIdle()
    {
        m_RooterAI.Move();
    }

}
