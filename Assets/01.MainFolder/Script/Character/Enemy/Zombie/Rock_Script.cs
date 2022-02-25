using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Script : MonoBehaviour
{
    public GameObject m_ParticlePrefab;
    private GameObject m_ParticleValue;
    private PlayerStatus m_PlayerStatus;

    private Rigidbody m_Rigidbody;
    private SoundScript m_SoundScript;

    public GameObject m_Mesh;

    private bool m_bDestroy = false;

    private float m_fAccTime = 0f;
    private int m_iAtk = 5;

    void Awake()
    {
        m_SoundScript = GetComponent<SoundScript>();
        m_PlayerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_fAccTime >= 8.0f)
        {
            Destroy(gameObject);
        }

        if (!m_ParticleValue && m_bDestroy)
        {
            Destroy(gameObject);
        }

        m_fAccTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (m_iAtk > 0)
            m_iAtk -= 1;

        BossAI bossAI;
        if (collision.transform.TryGetComponent<BossAI>(out bossAI))
            return;

        if (collision.gameObject == m_PlayerStatus.gameObject)
        {
            collision.gameObject.GetComponent<PlayerStatus>().SetDamage((int)m_Rigidbody.velocity.magnitude * m_iAtk);
            m_Mesh.SetActive(false);
            m_bDestroy = true;
            m_ParticleValue = Instantiate(m_ParticlePrefab, transform);
            m_SoundScript.Play_JustOne(1);
            return;
        }

        m_SoundScript.Play_JustOne(0);
        m_ParticleValue = Instantiate(m_ParticlePrefab, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_SoundScript.Play_JustOne(1);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<IDamageReceiver>().GiveDamage((int)m_Rigidbody.velocity.magnitude * 100);
            m_ParticleValue = Instantiate(m_ParticlePrefab, transform);
        }
    }
}
