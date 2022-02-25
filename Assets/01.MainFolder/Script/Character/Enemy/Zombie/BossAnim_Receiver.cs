using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim_Receiver : MonoBehaviour
{
    private BossAI m_BossAI;
    private Animator m_Animator;
    private EnemyStatus m_EnemyStatus;

    // Start is called before the first frame update
    void Awake()
    {
        transform.parent.TryGetComponent(out m_BossAI);
        TryGetComponent(out m_Animator);
        TryGetComponent(out m_EnemyStatus);
    }

    public void StartThrow()
    {
        m_BossAI.m_SoundScript.Play_JustOne(6);
    }

    public void ThrowRock()
    {
        m_BossAI.m_InstanceRock.transform.parent = null;
        Collider collider;
        m_BossAI.m_InstanceRock.TryGetComponent(out collider);
        collider.enabled = true;
        collider.isTrigger = false;
        Rigidbody rigidbody;
        m_BossAI.m_InstanceRock.TryGetComponent(out rigidbody);
        rigidbody.isKinematic = false;
        rigidbody.AddForce( m_BossAI.transform.forward * 0.9f * m_BossAI.m_vDir.magnitude, ForceMode.Impulse);
    }

    public void ThrowEnd()
    {
        m_BossAI.m_NavMeshAgent.isStopped = false;
        m_BossAI.m_bThrow = false;
        m_Animator.SetBool("Throw", false);
    }

    public void BossDie()
    {
        m_BossAI.Die();
    }

    public void DeadStart()
    {
        m_BossAI.m_bAngry = false;
        m_Animator.speed = 0.8f;
    }
    
    public void Roaring()
    {
        int _iRoarClip = Random.Range(7, 8);
        m_BossAI.m_SoundScript.Play_JustOne(_iRoarClip);
    }

    public void EndRoar()
    {
        m_BossAI.m_NavMeshAgent.isStopped = false;
        m_BossAI.m_NavMeshAgent.speed = 7.5f;
        m_BossAI.m_eBossPattern = BossAI.BossPattern.Pattern_Move01;
    }

    public void EndAttack()
    {
        m_BossAI.m_NavMeshAgent.isStopped = false;
        m_BossAI.m_bAttack = false;
    }

    public void CreateAttCollider()
    {
        m_BossAI.m_SoundScript.Play_JustOne(m_BossAI.m_iAtkAudioClip);

        Collider[] colliders = Physics.OverlapSphere(m_BossAI.m_RockSocket.transform.position, 4f,
            (1 << LayerMask.NameToLayer("Player")));

        foreach (Collider collider in colliders)
        {
            IDamageReceiver receiver;
            collider.TryGetComponent(out receiver);
            if (null != receiver)
            {
                receiver.GiveDamage(m_BossAI.m_EnemyStatus.m_Status.fAttack);
            }
        }
    }

    public void Charge_Start()
    {
        int _iclipNum = Random.Range(9, 10);
        m_BossAI.m_SoundScript.Play_JustOne(_iclipNum);
        m_Animator.SetTrigger("Charge");
    }

    public void Charge_Stop()
    {
        m_BossAI.m_bCharge = false;
    }

    public void KneeDown()
    {
        m_BossAI.m_SoundScript.Play_JustOne(17);
    }

    public void KneelingStart()
    {
        int _iclipNum = Random.Range(13, 14);
        m_BossAI.m_SoundScript.Play_JustOne(_iclipNum);
    }

    public void Crush_StandUp()
    {
        m_Animator.SetBool("Walk", true);
        m_BossAI.m_NavMeshAgent.isStopped = false;
    }

    public void StandUpEnd()
    {
        m_BossAI.m_bCrush = false;
        m_BossAI.m_eBossPattern = BossAI.BossPattern.Pattern_Move01;
    }

    public void StopEnd()
    {
        m_BossAI.m_NavMeshAgent.isStopped = false;
        m_BossAI.m_eBossPattern = BossAI.BossPattern.Pattern_Move01;
    }

    public void StartCrush()
    {
        int _iclipNum = Random.Range(11, 12);
        int _iclipNum1 = Random.Range(15, 16);
        m_BossAI.m_SoundScript.Play_JustOne(_iclipNum);
        m_BossAI.m_SoundScript.Play_JustOne(_iclipNum1);
        m_BossAI.m_bCharge = false;
        m_BossAI.m_bCrush = true;
    }

    public void EndCrush()
    {
        m_BossAI.m_eBossPattern = BossAI.BossPattern.Pattern_Kneeling;
    }

    public void BossStep()
    {
        int _iclipNum = Random.Range(0, 2);
        m_BossAI.m_SoundScript.Play_JustOne(_iclipNum);
    }
}
