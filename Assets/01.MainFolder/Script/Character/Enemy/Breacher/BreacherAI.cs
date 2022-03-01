using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreacherAI : MonoBehaviour, IAlertEventReceiver
{
    public GameObject m_ExplodeParticle;

    private AIViewPerception m_AIViewPercetion;
    private Transform m_playerTransform;
    private Transform m_TargetTransform;
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private BreacherMaterialCtrl m_materialCtrl;
    private EnemyStatus m_EnemyStatus;
    private const float m_fEXPLODE_RANGE = 10f;
    //Movement Var
    private float m_fWalkSpeed;
    static private float m_fRunSpeed = 10f;

    public GameObject m_Mesh;
    public SoundScript m_Soundscript;
    private GameObject m_ParticleCopy;
    private bool m_bDead = false;

    public bool m_bReadyToAttack { get; set; } = true;
    public bool m_bIsMoveable { private get; set; } = true;

    void Awake()
    {
        m_playerTransform = GameObject.Find("Player").transform;
        TryGetComponent(out m_NavMeshAgent);
        m_Animator = GetComponent<Animator>();
        TryGetComponent(out m_AIViewPercetion);
        m_fWalkSpeed = m_NavMeshAgent.speed;
        TryGetComponent(out m_materialCtrl);
        TryGetComponent(out m_EnemyStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bDead && !m_ParticleCopy)
        {
            Destroy(gameObject);
            return;
        }

        ChangeSpeed(m_Animator.GetBool("IsAlert"));
        FollowToTarget();
        CheckOnNavMeshLink();
    }

    private void FollowToTarget()
    {
        if (!m_bReadyToAttack)
        {
            m_NavMeshAgent.isStopped = true;
            return;
        }
        m_TargetTransform = m_playerTransform;
        if (AIViewPerception.Alert.Alert == m_AIViewPercetion.m_eAlert)
        {
            Vector3 vDir;

            vDir = m_TargetTransform.position - transform.position;

            if (2.8f >= vDir.magnitude)
            {
                m_Animator.SetBool("IsWalk", false);
                AttackToPlayer();
                m_NavMeshAgent.isStopped = true;
            }
            else
            {
                if(m_bIsMoveable)
                {
                    MoveToTarget();
                }
                else
                {
                    m_NavMeshAgent.isStopped = true;
                }
            }
        }
        else
        {
            m_Animator.SetBool("IsWalk", false);
            m_NavMeshAgent.isStopped = true;
        }
    }
    public void Explode()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, m_fEXPLODE_RANGE, LayerMask.GetMask("Player"));
        if(0 < colliders.Length)
        {
            IDamageReceiver damageReceiver;
            colliders[0].TryGetComponent(out damageReceiver);
            float fDist = Vector3.Distance(colliders[0].transform.position, transform.position);
            float fDamage = fDamage = (m_fEXPLODE_RANGE - fDist) / m_fEXPLODE_RANGE * m_EnemyStatus.m_Status.fAttack;
            damageReceiver.GiveDamage(fDamage);
        }

        if (!m_bDead)
        {
            int _Clipnum = UnityEngine.Random.Range(0, 2);
            m_Soundscript.Play_JustOne(_Clipnum);
            m_ParticleCopy = Instantiate(m_ExplodeParticle, transform.position, transform.rotation);
            m_bDead = true;
        }
    }

    private void CheckOnNavMeshLink()
    {
        if (m_NavMeshAgent.isOnOffMeshLink && m_bIsMoveable)
        {
            m_Animator.SetTrigger("VaultOver");
            m_bIsMoveable = false;
            m_NavMeshAgent.updateRotation = false;
            m_NavMeshAgent.updatePosition = false;
            transform.LookAt(m_NavMeshAgent.currentOffMeshLinkData.endPos);
        }        
    }

    public void TeleportToMeshLinkEnd()
    {
        m_NavMeshAgent.Warp(transform.GetChild(0).position);
        m_NavMeshAgent.updateRotation = true;
        m_NavMeshAgent.updatePosition = true;
    }


    void AttackToPlayer()
    {
        if(m_bReadyToAttack)
        {
            m_Animator.SetTrigger("Attack");
            transform.LookAt(m_TargetTransform);
            m_bIsMoveable = false;
            m_bReadyToAttack = false;
            m_materialCtrl.m_bIsExploding = true;
        }
    }
    private void MoveToTarget()
    {
        m_Animator.SetBool("IsWalk", true);
        m_NavMeshAgent.SetDestination(m_TargetTransform.position);
        m_NavMeshAgent.isStopped = false;
    }

    public void AlertAwaken()
    {
        
        //m_Animator.SetTrigger("Scream");
        //m_bIsMoveable = false;
    }
    public void AlertSafe()
    {
        m_Animator.SetBool("IsAlert", false);
    }
    public void ChangeSpeed(bool _IsRun)
    {
        m_NavMeshAgent.speed = _IsRun ? m_fRunSpeed : m_fWalkSpeed;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
