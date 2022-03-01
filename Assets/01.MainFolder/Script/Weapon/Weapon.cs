using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { AR, SG, SR, HG, Melee, End }
    public WeaponList.WeaponName m_eWeaponName;
    public WeaponCtrl.Info m_WeaponInfo;

    public string m_strName;
    public Sprite m_ImgWeaponIcon;
    public Sprite m_ImgAmmoIcon;
    public Light m_FlashLight;
    public GameObject m_LaserPointer;
    public SoundScript m_SoundScript;

    public bool m_bDropWeap = false;
    
    //Raycast Var
    private GunCtrl m_WeaponCtrl = null;
    private Camera m_Camera;

    // Start is called before the first frame update
    void Awake()
    {
        m_WeaponCtrl = GunCtrl.Create(m_WeaponInfo, transform);
        m_Camera = Camera.main;
        m_strName = WeaponList.m_tWeapInfo[(int)m_eWeaponName]._strName;
        m_ImgWeaponIcon = WeaponList.m_tWeapInfo[(int)m_eWeaponName]._WeapImage;
        m_ImgAmmoIcon = WeaponList.m_tWeapInfo[(int)m_eWeaponName]._AmmoImage;
    }
    private void Start()
    {
        m_SoundScript = GetComponent<SoundScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!m_bDropWeap)
        //{
        //    LookEndOfCam();
        //}

        m_WeaponCtrl.Update();
        //Transform tempTransform = transform.parent;
    }

    public void Fire()
    {
        m_WeaponCtrl.Fire();
    }

    public void StopFire()
    {
        m_WeaponCtrl.StopFire();
    }

    public void Reload()
    {
        m_WeaponCtrl.Reloading();
    }

    public void FlashLight_OnOff(bool _OnOff)
    {
        m_FlashLight.enabled = _OnOff;
    }

    private void LookEndOfCam()
    {
        transform.LookAt(m_Camera.transform.position + m_Camera.transform.forward * GunCtrl.m_fRayDistance);

        RaycastHit hit;
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, GunCtrl.m_fRayDistance, 1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Background")))
        {
            transform.LookAt(hit.point);
        }            
    }

    public uint GetTotalAmmo()
    {
        return m_WeaponCtrl.GetTotalAmmo();
    }

    public uint GetAmmo()
    {
        return m_WeaponCtrl.GetAmmo();
    }

    public PlayerStatus.Item Get_ItemType()
    {
        return m_WeaponCtrl.Get_ItemType();
    }
    public bool GetIsMelee()
    {
        return m_WeaponCtrl.GetIsMelee();
    }

    public GameObject GetLaserPointer()
    {
        return m_LaserPointer;
    }

    public uint GetMag()
    {
        return m_WeaponCtrl.GetMag();
    }

    public void Set_Acc(bool _Aim, float _Value)
    {
        m_WeaponCtrl.Set_Arms_Accuracy(_Aim, _Value);
    }

    //public void SetLaserPointer(LaserPointer _laserpointer)
    //{
    //    m_WeaponCtrl.SetLaserPointer(_laserpointer);
    //}

    //public Transform GetMuzzleTransform()
    //{
    //    return m_WeaponCtrl.GetMuzzleTransform();
    //}

    //public void SetMuzzleTransform(Transform _MuzzleTrans)
    //{
    //    m_WeaponCtrl.SetMuzzleTransform(_MuzzleTrans);
    //}
}
