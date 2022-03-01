using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour, IDamageReceiver
{
    public PlayerStatus m_PlayerStatus;
    public float m_fDamageMultiply = 1f;

    public void GiveDamage(float _fDamage)
    {
        m_PlayerStatus.GiveDamage(_fDamage * m_fDamageMultiply);
    }
}
