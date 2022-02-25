using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : ThrowWeapon
{
    //Grenade Info
    private const float m_fDamage = 300f;
    private const float m_fRadius = 20f;
    private const float m_fMaxCookTime = 5f;
    private float m_fCookTime = m_fMaxCookTime;
    private bool m_bDestroy = false;
    private GameObject m_CopyParticle;

    private FragGrenade() { }

    public static FragGrenade Create(GameObject gameObject, GameObject _particleSystem, SoundScript _soundscript, GameObject _Mesh)
    {
        FragGrenade fragGrenade = new FragGrenade();
        fragGrenade.SetGameObject(gameObject);
        fragGrenade.SetExplodeParticle(_particleSystem);
        fragGrenade.SetSoundScript(_soundscript);
        fragGrenade.SetMesh(_Mesh);
        return fragGrenade;
    }
    //
    private bool m_bPinOff = false;
    // 절대 MonoBehavior에게 상속받지 않았음!! 따라서 자동실행이 아님!!
    public void Start()
    {
        
    }

    // 절대 MonoBehavior에게 상속받지 않았음!! 따라서 자동실행이 아님!!
    public void Update()
    {
        UpdateTime();

        if (m_bDestroy && !m_CopyParticle)
        {
            Destroy(gameObject);
        }
    }


    private void UpdateTime()
    {
        if (m_bPinOff)
        {
            m_fCookTime -= Time.deltaTime;
        }
        if (m_fCookTime <= 0 && !m_bDestroy)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, m_fRadius,
            (1 << LayerMask.NameToLayer("Enemy")) |
            (1 << LayerMask.NameToLayer("Player")) |
            (1 << LayerMask.NameToLayer("Explosive"))
            );
        foreach (Collider collider in colliders)
        {
            //collider.gameObject.TryGetComponent
            IDamageReceiver receiver;
            collider.TryGetComponent(out receiver);
            if(null != receiver)
            {
                receiver.GiveDamage(m_fDamage);
            }
        }
        GameObject gExplodeParticle = Instantiate(m_PrefabExplodeParticle, gameObject.transform.position, gameObject.transform.rotation);

        if (!m_bDestroy)
        {
            m_Mesh.SetActive(false);
            m_SoundScript.Play_JustOne(0);
            m_CopyParticle = gExplodeParticle;
            m_bDestroy = true;
        }
    }
    public override void SetPinOff(bool _bIsPinOff)
    {
        m_bPinOff = _bIsPinOff;
    }

    public override void Throw()
    {
        base.Throw();
    }

    public override void PutIn()
    {
        m_fCookTime = m_fMaxCookTime;
    }
}

