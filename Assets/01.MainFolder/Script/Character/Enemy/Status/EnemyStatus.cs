using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private Animator m_Animator;
    private float m_fTickDam = 5;

    public GameObject m_FlameEffect;
    public Status m_Status;

    public bool m_bIsAttacking { get; set; } = false;
    //
    // Start is called before the first frame update
    void Awake()
    {
        if (!TryGetComponent(out m_Animator))
        {
            transform.GetChild(0).TryGetComponent(out m_Animator);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveDamage(float _fDamage)
    {
        m_Status.fHp -= _fDamage;
        if (m_Status.fHp <= 0f)
        {
            m_Animator.SetTrigger("Dead");
        }
    }

    public void GiveTickDamage()
    {
        GiveDamage(m_fTickDam);
    }

    public void Start_Burn()
    {
        m_FlameEffect.SetActive(true);
        InvokeRepeating("GiveTickDamage", 0f, 1f);
    }
}
