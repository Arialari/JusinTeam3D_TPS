using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead_Script : MonoBehaviour
{
    public BossAI m_BossAI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            if (m_BossAI.m_eBossPattern == BossAI.BossPattern.Pattern_Charge)
            {
                m_BossAI.m_eBossPattern = BossAI.BossPattern.Pattern_Crush;
                m_BossAI.m_NavMeshAgent.isStopped = true;
            }
        }
    }
}
