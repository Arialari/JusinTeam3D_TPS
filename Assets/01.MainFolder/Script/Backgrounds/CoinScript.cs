using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int m_iHp = 1;
    public GameObject m_Mesh;
    public GameObject m_CoinParticle;
    private GameObject m_ParticleCopy;
    private int m_iMoney = 800;

    private bool m_bDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bDead && !m_ParticleCopy)
        {
            Destroy(gameObject);
        }

        CreateParticle();
    }

    public int Take_Coin()
    {
        if (m_Mesh.activeSelf)
            Set_Damage();

        else
            return 0;

        return m_iMoney;
    }

    private void Set_Damage()
    {
        m_iHp -= 1;
        m_Mesh.SetActive(false);
        GetComponent<Collider>().enabled = false;
        m_ParticleCopy = Instantiate(m_CoinParticle, transform);
    }

    private void CreateParticle()
    {
        if (m_iHp == 0)
        {
            m_bDead = true;
        }
    }
}
