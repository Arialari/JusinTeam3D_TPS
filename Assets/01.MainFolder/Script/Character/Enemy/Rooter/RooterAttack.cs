using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RooterAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_fInitY;
    private const float m_fMAX_Y = 1.6f;
    private const float m_fSPEED = 2.0f;
    private const float m_fLIFE_SPAN = 3.0f;
    private const float m_fRANGE = 2.0f;
    private float m_fLifeTime = 0.0f;
    private NavMeshObstacle m_NavMeshObstacle; 
    private Transform m_PlayerTransform;
    void Start()
    {
        m_fInitY = transform.position.y;
        TryGetComponent(out m_NavMeshObstacle);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_fLifeTime >= m_fLIFE_SPAN)
        {
            Destroy(gameObject);
            return;
        }
        UpdateY();
    }

    private void LateUpdate()
    {
        if(m_PlayerTransform)
        {
            m_PlayerTransform.position = transform.position + new Vector3(0.0f, -2.0f, 0.0f);
            
        }
    }

    private void UpdateY()
    {
        if (transform.position.y < m_fInitY + m_fMAX_Y)
        {
            transform.position += new Vector3(0.0f, Time.deltaTime * m_fSPEED, 0.0f);
        }
        else
        {
            if(!m_NavMeshObstacle.enabled)
            {
                StunPlayer();
            }
            m_fLifeTime += Time.deltaTime;
        }
    }
    private void StunPlayer()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, m_fRANGE, LayerMask.GetMask("Player"));
        if(colliders.Length > 0)
        {
            m_PlayerTransform = colliders[0].transform;
        }
        else
        {
            m_NavMeshObstacle.enabled = true;
        }
    }
}
