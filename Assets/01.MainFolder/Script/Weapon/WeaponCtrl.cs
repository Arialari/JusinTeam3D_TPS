using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : Object
{
    [System.Serializable]
    public struct Info
    {
        public float fAtk;
        public float fRatio;
        public bool bIsMelee;
        public uint iMag;
        public bool bIsAuto;
        public Weapon.Type eType;
        public PlayerStatus.Item eItemType;
    }

    protected Info m_WeaponInfo; // Origin Var
    protected Info m_WeaponStatus; // Cur Var, Use THIS!!
    protected Transform m_weaponTransform;
    protected PlayerStatus m_PlayerStatus;

    protected GameObject m_BloodSprayEffect;

    //Status Var;
    protected const uint m_fMaxTotalAmmo = 999;
    protected uint m_iAmmo;

    //Fire Var
    //private LineRenderer m_lineRenderer;
    public const float m_fRayDistance = 100f;
    protected bool m_bNonAutoCoolDown = false;
    virtual public void Initialize()
    {
        m_BloodSprayEffect = Resources.Load("BloodSprayEffect", typeof(GameObject)) as GameObject;
        GameObject Player = GameObject.Find("Player");
        Player.TryGetComponent(out m_PlayerStatus);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void Fire()
    {

    }

    virtual public void StopFire()
    {
    }

    virtual public void Reloading()
    {

    }

    public void SetInitInfo(Info _Info)
    {
        m_WeaponStatus = m_WeaponInfo = _Info;
        m_iAmmo = _Info.iMag;
    }
    public void SetWeaponTransform(Transform transform)
    {
        m_weaponTransform = transform;
    }

    public void SetTotalAmmo(uint _iTotalAmmo)
    {
        if (m_fMaxTotalAmmo < _iTotalAmmo)
        {
            _iTotalAmmo = m_fMaxTotalAmmo;
        }
        m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] = _iTotalAmmo;
    }

    public void AddTotalAmmo(uint _iTotalAmmo)
    {
        m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] += _iTotalAmmo;
        if (m_fMaxTotalAmmo < m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType])
        {
            m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] = m_fMaxTotalAmmo;
        }
    }

    public bool GetIsMelee()
    {
        return m_WeaponStatus.bIsMelee;
    }
    public float GetRatio()
    {
        return m_WeaponStatus.fRatio;
    }
    public float GetAtk()
    {
        return m_WeaponStatus.fAtk;
    }
    public float GetDamage()
    {
        return m_WeaponStatus.fAtk;
    }

    public uint GetAmmo()
    {
        return m_iAmmo;
    }
    public uint GetTotalAmmo()
    {
        return m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType];
    }

    public PlayerStatus.Item Get_ItemType()
    {
        return m_WeaponStatus.eItemType;
    }

    public Transform GetWeaponTranform()
    {
        return m_weaponTransform;
    }

    public uint GetMag()
    {
        return m_WeaponStatus.iMag;
    }

    virtual public void Set_Arms_Accuracy(bool _Aim, float _Value)
    {
        
    }
}
