using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid_Script : MonoBehaviour, IMedicineScript
{
    public GameObject m_PrefabHealingParticle;
    private FirstAid m_firstaid;

    void Awake()
    {
        m_firstaid = FirstAid.Create(gameObject, m_PrefabHealingParticle);
    }

    void Update()
    {
        m_firstaid.Update();
    }

    public Medicine GetMedicine()
    {
        return m_firstaid;
    }
}
