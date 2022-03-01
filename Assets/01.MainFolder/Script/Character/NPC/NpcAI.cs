using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : MonoBehaviour, IInteractE
{
    public Vector3 m_DestroyPosition;
    private Transform m_Playertransform;
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private Transform m_DestinationTransform;
    void Awake()
    {
        transform.GetChild(0).TryGetComponent(out m_Animator);
        TryGetComponent(out m_NavMeshAgent);
        m_Playertransform = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_DestinationTransform)
            return;
        if (Vector3.Distance(m_DestinationTransform.position, transform.position) < 7f)
        {
            m_NavMeshAgent.isStopped = true;
            m_Animator.SetBool("Walk", false);
        }
        else
        {
            m_NavMeshAgent.SetDestination(m_DestinationTransform.position);
            m_NavMeshAgent.isStopped = false;
            m_Animator.SetBool("Walk", true);
        }
        if(Vector3.Distance(m_DestroyPosition, transform.position) < 10f)
        {
            Destroy(gameObject);
        }
    }
    public void SetDestination(Transform transform)
    {
        if (transform)
        {
            m_DestinationTransform = transform;
            m_NavMeshAgent.isStopped = false;
        }
        else
            m_NavMeshAgent.isStopped = true;
    }
    public void SetDestination(Vector3 position)
    {
        m_NavMeshAgent.SetDestination(position);
        m_NavMeshAgent.isStopped = false;
    }

    public void InteractE()
    {
        ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Get_Hastage);
        SetDestination(m_Playertransform);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_DestroyPosition, 10f);
    }
#endif
}
