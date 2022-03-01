using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveOBJ_Script : MonoBehaviour
{
    public struct tagBarrelStatus
    {
        public int iAtt;
        public int iMaxHP;
        public int iHP;
    }

    public tagBarrelStatus m_tBarrelStat;
    public GameObject m_BurningParticle;
    public GameObject m_SmokeParticle;
    public GameObject m_ExplosionParticle;
    public GameObject m_Mesh;
    public SoundScript m_SoundScript;

    private GameObject m_CopyParticle0;
    private GameObject m_CopyParticle1;
    private GameObject m_CopyParticle2;

    private float m_fAccTime;
    private bool m_bBurn = false;
    private bool m_bDestroy = false;

    // Start is called before the first frame update
    void Awake()
    {
        m_SoundScript = GetComponent<SoundScript>();
        m_tBarrelStat.iMaxHP = 5;
        m_tBarrelStat.iHP = m_tBarrelStat.iMaxHP;
        m_tBarrelStat.iAtt = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bBurn)
        {
            Burning();
        }

        if (m_bDestroy)
        {
            if (m_CopyParticle2 == null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start_Burn()
    {
        m_CopyParticle0 = Instantiate(m_BurningParticle, transform);
        m_CopyParticle0.transform.localScale = new Vector3(1, 1, 1);
        m_CopyParticle0.transform.localPosition = new Vector3(0, 3, 0);
        m_CopyParticle1 = Instantiate(m_SmokeParticle, transform);
        m_CopyParticle1.transform.localScale = new Vector3(1, 1, 1);
        m_CopyParticle1.transform.localPosition = new Vector3(0, 3, 0);

        m_SoundScript.Play_LoopOn();
        m_SoundScript.Play_JustOne(1);

        m_bBurn = true;
    }

    private void Burning()
    {
        m_fAccTime += Time.deltaTime;

        if (m_fAccTime > 1f)
        {
            m_fAccTime = 0f;
            m_tBarrelStat.iHP -= 1;
        }

        if (m_tBarrelStat.iHP <= 0)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 20f,
               (1 << LayerMask.NameToLayer("Enemy")) |
               (1 << LayerMask.NameToLayer("Player"))
               );
        foreach (Collider collider in colliders)
        {
            IDamageReceiver receiver;
            collider.TryGetComponent(out receiver);
            if (null != receiver)
            {
                receiver.GiveDamage(m_tBarrelStat.iAtt);
            }
        }

        m_Mesh.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        m_CopyParticle2 = Instantiate(m_ExplosionParticle, transform);
        m_CopyParticle2.transform.localScale = new Vector3(3, 3, 3);
        m_CopyParticle2.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(m_CopyParticle0.gameObject);
        Destroy(m_CopyParticle1.gameObject);

        m_SoundScript.Play_LoopOff();
        m_SoundScript.Play_JustOne(0);

        m_bDestroy = true;
        m_bBurn = false;
    }

    public void SetDamage()
    {
        m_tBarrelStat.iHP--;

        if (m_tBarrelStat.iHP == 4)
        {
            Start_Burn();
        }
    }
}
