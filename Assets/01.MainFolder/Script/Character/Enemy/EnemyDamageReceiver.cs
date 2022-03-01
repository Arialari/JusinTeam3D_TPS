using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour, IDamageReceiver
{
    public EnemyStatus m_EnemyStatus;
    public float m_fDamageMultiply = 1f;

    public void GiveDamage(float _fDamage)
     {
        m_EnemyStatus.GiveDamage(_fDamage * m_fDamageMultiply);
    }

    public void Start_Burn()
    {
        m_EnemyStatus.Start_Burn();
    }
}
