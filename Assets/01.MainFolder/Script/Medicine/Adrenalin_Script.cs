using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenalin_Script : MonoBehaviour, IMedicineScript
{
    public GameObject m_PrefabHealingParticle;
    private Adrenalin m_Adrenalin;

    void Awake()
    {
        m_Adrenalin = Adrenalin.Create(gameObject, m_PrefabHealingParticle);
    }

    void Update()
    {
        m_Adrenalin.Update();
    }

    public Medicine GetMedicine()
    {
        return m_Adrenalin;
    }
}
