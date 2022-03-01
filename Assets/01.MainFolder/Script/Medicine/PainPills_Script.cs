using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainPills_Script : MonoBehaviour, IMedicineScript
{
    public GameObject m_PrefabHealingParticle;
    private PainPills m_painpills;

    void Awake()
    {
        m_painpills = PainPills.Create(gameObject, m_PrefabHealingParticle);
    }

    void Update()
    {
        m_painpills.Update();
    }

    public Medicine GetMedicine()
    {
        return m_painpills;
    }
}
