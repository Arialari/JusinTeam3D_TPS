using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Object
{
    protected GameObject m_gameObject;
    protected GameObject m_PrefabHealingParticle;
    protected string m_strObjName = "";
    protected int m_iHealingAmount = 0;
    protected float m_fUsingTime = 0f;

    public Using_GageBar m_UsingGageBar;

    private PlayerStatus m_PlayerStat;
    private float m_fUsingAccTime = 0f;
    private bool m_bUsing = false;
    private int m_iUsingCount = 1;

    public virtual void Initiate()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    public virtual void Update()
    {
        if (m_bUsing && m_iUsingCount == 1)
        {
            if (m_PlayerStat.m_iMedicineNUM == (int)WeaponList.ItemName.FirstAid)
                UseFirstAid();

            if (m_PlayerStat.m_iMedicineNUM == (int)WeaponList.ItemName.PainPill ||
                m_PlayerStat.m_iMedicineNUM == (int)WeaponList.ItemName.Adrenalin)
            {
                UseTemporaryMedicine();
            }
        }

        else
            StopUsing_Hold();
    }

    public virtual void UseFirstAid()
    {
        m_fUsingAccTime += Time.deltaTime;

        if (m_fUsingAccTime / m_fUsingTime >= 1.0f)
        {
            Instantiate(m_PrefabHealingParticle, (m_PlayerStat.transform.position + new Vector3(0, 4.5f, 0)), m_PlayerStat.transform.rotation);
            m_PlayerStat._HP += m_iHealingAmount;
            m_PlayerStat.m_iAmmo_Medicine -= 1;
            m_iUsingCount = 0;
            m_fUsingAccTime = 0f;
        }
    }

    public virtual void UseTemporaryMedicine()
    {
        m_fUsingAccTime += Time.deltaTime;

        if (m_fUsingAccTime / m_fUsingTime >= 1.0f)
        {
            Instantiate(m_PrefabHealingParticle, (m_PlayerStat.transform.position + new Vector3(0, 4.5f, 0)), m_PlayerStat.transform.rotation);
            m_PlayerStat.m_iTemporaryHP_Point = m_iHealingAmount;
            m_PlayerStat._iUsePainPhillCount = 1;
            m_PlayerStat._bUsePainPhill = true;
            m_PlayerStat.m_iAmmo_Medicine -= 1;
            m_iUsingCount = 0;
            m_fUsingAccTime = 0f;
        }
    }

    public void SetGameObject(GameObject _gameObject)
    {
        m_gameObject = _gameObject;
    }

    public void SetHealingParticle(GameObject _particleSystem)
    {
        m_PrefabHealingParticle = _particleSystem;
    }

    public virtual void StopUsing_Hold()
    {
        m_fUsingAccTime = 0f;
        m_bUsing = false;
    }

    public virtual void StopUsing()
    {
        m_fUsingAccTime = 0f;
        m_bUsing = false;
        m_iUsingCount = 1;
    }

    public void Set_bUsing(bool _bUsing)
    {
        m_bUsing = _bUsing;
    }

    public bool Get_bUsing()
    {
        return m_bUsing;
    }

    public int Get_UsingCount()
    {
        return m_iUsingCount;
    }

    public virtual void ChangeTo_Parameter_ofAnimator(Animator _Animator, bool _Param)
    {
 
    }

    public float Get_UsingTime()
    {
        return m_fUsingTime;
    }

    public string Get_MedicineName()
    {
        return m_strObjName;
    }

    public void Set_MedicineName(string _strName)
    {
        m_strObjName = _strName;
    }
}
