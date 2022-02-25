using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBombScript : MonoBehaviour, IThrowWeaponScript
{
    public GameObject m_PrefabExplosion;
    private PipeBomb m_PipeBomb;

    private GeneralZombieAI m_GeneralZombieAI;

    // Start is called before the first frame update
    void Awake()
    {
        m_PipeBomb = PipeBomb.Create(gameObject, m_PrefabExplosion);
        m_GeneralZombieAI = null;
    }

    // Update is called once per frame
    void Update()
    {
        m_PipeBomb.Update();
    }

    public ThrowWeapon GetThrowWeapon()
    {
        return m_PipeBomb;
    }

    public void ChangeTarget()
    {
        if (gameObject.transform.parent == null)
        {
            m_GeneralZombieAI.SetTarget(transform);
        }
    }
}
