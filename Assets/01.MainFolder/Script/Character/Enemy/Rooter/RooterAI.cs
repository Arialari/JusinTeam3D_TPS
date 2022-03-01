using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RooterAI : MonoBehaviour, IAlertEventReceiver
{
    public GameObject m_AttackParticle;
    public GameObject m_TeleportParticle;
    public GameObject m_TeleportEndParticle;
    private AIViewPerception m_AIViewPercetion;
    private Transform m_playerTransform;
    private Transform m_TargetTransform;
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private EnemyStatus m_EnemyStatus;

    public SoundScript m_Soundscript;

    private enum State { Idle, Move, Attack }
    private State m_eState = State.Idle;
    private State m_ePrevState = State.Idle;
    private enum MoveState { None, Start, End }
    private MoveState m_eMoveState = MoveState.None;

    private bool m_bAlart = false;
    private const float m_fTELEPORT_RANGE = 10f;
    private const float m_fATTACK_RANGE = m_fTELEPORT_RANGE;
    private LayerMask m_TeleportLayerMask;

    public bool m_bReadyToAttack { get; set; } = true;
    public bool m_bIsMoveable { private get; set; } = true;

    void Awake()
    {
        m_playerTransform = GameObject.Find("Player").transform;
        TryGetComponent(out m_NavMeshAgent);
        m_Animator = GetComponent<Animator>();
        TryGetComponent(out m_AIViewPercetion);
        TryGetComponent(out m_EnemyStatus);
        m_TeleportLayerMask = LayerMask.GetMask("Background", "StaticInteractOBJ");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
    public void Attack()
    {
        if(Vector3.Distance(transform.position,m_playerTransform.position) <= m_fATTACK_RANGE)
        {
            Instantiate(m_AttackParticle,m_playerTransform.position, m_playerTransform.rotation);
        }
        else
        {
            Vector3 spawnPosition = transform.position + Vector3.Normalize(m_playerTransform.position - transform.position) * m_fATTACK_RANGE;
            Instantiate(m_AttackParticle, spawnPosition, m_playerTransform.rotation);
        }
    }
    public void AlertAwaken()
    {
        m_Animator.SetTrigger("Scream");
        m_bIsMoveable = false;
    }
    public void AlertSafe()
    {
        m_Animator.SetBool("IsAlert", false);
    }
    public void StartTeleport()
    {
        float fRand = UnityEngine.Random.Range(0.0f,m_fTELEPORT_RANGE);
        float fZ = Mathf.Sqrt(Mathf.Pow(m_fTELEPORT_RANGE, 2) - Mathf.Pow(fRand, 2));
        float fSignX = 0.5 <= UnityEngine.Random.value ? 1.0f : -1.0f;
        float fSignZ = 0.5 <= UnityEngine.Random.value ? 1.0f : -1.0f;

        Vector3 vRandPosition = m_playerTransform.position + new Vector3(fSignX * fRand, 0.0f, fSignZ * fZ);
        Vector3 vDestinationPosition = vRandPosition;
        RaycastHit hit;
        if(Physics.Linecast(m_playerTransform.position,vRandPosition,out hit,m_TeleportLayerMask))
        {
            vDestinationPosition = hit.point;
        }
        m_NavMeshAgent.Warp(vDestinationPosition);
        m_eMoveState = MoveState.End;
        Instantiate(m_TeleportEndParticle,vDestinationPosition,transform.rotation);
    }
    public void EndTeleport()
    {
        m_eState = State.Attack;
        m_Animator.SetTrigger("Attack");
        m_eMoveState = MoveState.None;
    }
    public void SetAlert(bool _bIsAlert)
    {
        m_bAlart = _bIsAlert;
    }
    public void Move()
    {
        int _Clipnum = UnityEngine.Random.Range(0, 1);
        m_Soundscript.Play_JustOne(_Clipnum);
        m_Animator.SetTrigger("Move");
        m_eState = State.Move;
        m_eMoveState = MoveState.Start;
        Instantiate(m_TeleportParticle, transform.position, transform.rotation);
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateMovement()
    {
        switch (m_eState)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Move: if (m_eState != m_ePrevState) { Move(); } UpdateMove(); break;
            case State.Attack: break;
        }
        m_ePrevState = m_eState;
    }
    private void UpdateMove()
    {
        switch(m_eMoveState)
        {
            case MoveState.None: break;
            case MoveState.Start: break;
            case MoveState.End: break;
        }
    }

    private void UpdateIdle()
    {
        if(m_bAlart)
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        transform.LookAt(m_playerTransform);
    }

    
}
