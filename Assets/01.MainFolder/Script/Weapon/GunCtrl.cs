using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : WeaponCtrl
{
    private Transform m_MuzzleTransform;
    private GameObject m_GunExplosiveEffect;
    private GameObject m_CartridgeEjectEffect;
    private GameObject m_MuzzleFlashEffect;
    private LaserPointer m_LaserPointer;
    private Camera m_Camera;
    private Weapon m_PrimaryWeapon;
    private Camera m_GunCamera;
    //FireRatio Var
    private float m_fCoolTime2Fire;

    private float m_fArms_Accuracy = 0.02f;
    private float m_fPre_Accuracy = 0f;
    private bool m_bShot = false;
    private bool m_bAim = false;

    private GunCtrl() { }
    public override void Initialize()
    {
        base.Initialize();
        //m_lineRenderer.startWidth = m_lineRenderer.endWidth = 0.1f;
        //m_lineRenderer.material = Resources.Load("MAT_White", typeof(Material)) as Material;
        m_PrimaryWeapon = m_PlayerStatus.GetPrimaryWeapon();
        m_GunExplosiveEffect = Resources.Load("GunExplosionEffect", typeof(GameObject)) as GameObject;
        m_CartridgeEjectEffect = Resources.Load("CartridgeEjectEffect", typeof(GameObject)) as GameObject;
        m_MuzzleFlashEffect = Resources.Load("MuzzleFlashEffect", typeof(GameObject)) as GameObject;
        m_MuzzleTransform = m_weaponTransform.GetTransformInAllChildren("Muzzle");
        if(m_MuzzleTransform)
        {
            Transform transform = m_MuzzleTransform.GetTransformInAllChildren("LaserPointer");

            if (transform != null)
            {
                m_LaserPointer = m_MuzzleTransform.GetTransformInAllChildren("LaserPointer").GetComponent<LaserPointer>();
            }
        }
        Transform camTransform = m_weaponTransform.GetTransformInAllChildren("SniperZoomCam");
        if(camTransform)
        {
            m_GunCamera = camTransform.GetComponent<Camera>();
        }

        m_fPre_Accuracy = m_fArms_Accuracy;
    }

    public static GunCtrl Create(Info _Info, Transform _weaponTransform)
    {
        GunCtrl weaponCtrl = new GunCtrl();
        weaponCtrl.SetInitInfo(_Info);
        weaponCtrl.SetWeaponTransform(_weaponTransform);
        weaponCtrl.Initialize();
        return weaponCtrl;
    }

    public void Update()
    {
        UpdateTimeVariables();

        if (!m_bShot && m_fArms_Accuracy > m_fPre_Accuracy)
        {
            m_fArms_Accuracy -= Time.deltaTime;
        }
        else if (m_fArms_Accuracy < m_fPre_Accuracy)
        {
            m_fArms_Accuracy = m_fPre_Accuracy;
        }
    }

    private void UpdateTimeVariables()
    {
        m_fCoolTime2Fire -= Time.deltaTime;
        if (m_fCoolTime2Fire < 0f)
        {
            m_fCoolTime2Fire = 0f;
        }
    }

    public override void Fire()
    {
        if (m_fCoolTime2Fire == 0f && !m_bNonAutoCoolDown)
        {
            if (m_iAmmo > 0)
            {
                m_bShot = true;
                OnFire();
            }
            else
            {
                m_bShot = false;
                if (GetTotalAmmo() > 0)
                {
                    if (m_PlayerStatus.GetSelectedItem() == PlayerStatus.Item.Primary)
                    {
                        m_PlayerStatus.GetPrimaryWeapon().m_SoundScript.Play_JustOne(1);
                    }
                    else if (m_PlayerStatus.GetSelectedItem() == PlayerStatus.Item.Secondary &&
                        !m_PlayerStatus.GetSecondaryWeapon().GetIsMelee())
                    {
                        m_PlayerStatus.GetSecondaryWeapon().m_SoundScript.Play_JustOne(1);
                    }

                    m_PlayerStatus.SetAnimatorParameterBool("Reload", true);
                }
            }
            m_fCoolTime2Fire = GetRatio();
        }
    }
    public override void StopFire()
    {
        m_bShot = false;
        
        if (!m_WeaponInfo.bIsAuto)
        {
            m_bNonAutoCoolDown = false;
        }
    }

    public override void Reloading()
    {
        OnReload();
    }

    private void OnReload()
    {
        if (0 >= m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] || // Total == 0
            m_WeaponInfo.iMag == m_iAmmo) // Mag == Ammo
        {
            return;
        }
        else if ((m_WeaponInfo.iMag - m_iAmmo) > m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType]) // Mag > Total
        {
            m_iAmmo += m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType];
            m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] = 0;
        }
        else
        {
            m_PlayerStatus.m_iTotalAmmo[(int)m_WeaponInfo.eType] -= (m_WeaponInfo.iMag - m_iAmmo);
            m_iAmmo = m_WeaponInfo.iMag;
        }
    }

    private void OnFire()
    {
        if (m_PlayerStatus.GetSelectedItem() == PlayerStatus.Item.Secondary &&
            m_PlayerStatus.GetSecondaryWeapon().GetIsMelee())
        {
            m_PlayerStatus.GetSecondaryWeapon().m_SoundScript.Play_JustOne(0);
            m_PlayerStatus.SetAnimatorParameterTrigger("Melee");
            return;
        }

        if(Camera.main)
        {
            m_Camera = Camera.main;
        }
        else
        {
            m_Camera = m_GunCamera;
        }

        //Sound
        if (m_PlayerStatus.GetSelectedItem() == PlayerStatus.Item.Primary)
        {
            m_PlayerStatus.GetPrimaryWeapon().m_SoundScript.Play_JustOne(0);
        }
        else if (m_PlayerStatus.GetSelectedItem() == PlayerStatus.Item.Secondary &&
            !m_PlayerStatus.GetSecondaryWeapon().GetIsMelee())
        {
            m_PlayerStatus.GetSecondaryWeapon().m_SoundScript.Play_JustOne(0);
        }


        m_PlayerStatus.SetAnimatorParameterTrigger("Shot");
        --m_iAmmo;

        if (m_fArms_Accuracy < (m_fPre_Accuracy * 10f))
            Trans_Arms_Accuracy(0.005f);

        if (!m_WeaponInfo.bIsAuto)
        {
            m_bNonAutoCoolDown = true;
        }

        RaycastHit hit;

        if (m_WeaponInfo.eType == Weapon.Type.SG)
        {
            for (int iBulletvalue = 10; iBulletvalue > 0; iBulletvalue--)
            {
                float fRandomX = Random.Range(-0.05f, 0.05f);
                float fRandomY = Random.Range(-0.05f, 0.05f);
                float fRandomZ = Random.Range(-0.05f, 0.05f);
                Vector3 vShotDis = m_Camera.transform.forward + new Vector3(fRandomX, fRandomY, fRandomZ);
                
                if (Physics.Raycast(m_MuzzleTransform.position, vShotDis, out hit, m_fRayDistance, LayerMask.GetMask("Enemy","Background","Explosive","StaticInteractOBJ")))
                {
                    Debug.DrawRay(m_MuzzleTransform.position, vShotDis * hit.distance, Color.green, 0.3f);
                    Debug.DrawRay(hit.point, vShotDis * (m_fRayDistance - hit.distance), Color.red, 0.3f);

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        EnemyDamageReceiver damageReceiver;
                        hit.transform.TryGetComponent(out damageReceiver);
                        damageReceiver.GiveDamage(GetDamage());

                        Instantiate(m_BloodSprayEffect, hit.point, hit.transform.rotation);
                    }
                    else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                    {
                        Instantiate(m_GunExplosiveEffect, hit.point, hit.transform.rotation);
                    }
                    else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Explosive"))
                    {
                        Instantiate(m_GunExplosiveEffect, hit.point, hit.transform.rotation);
                        hit.transform.gameObject.GetComponent<ExplosiveOBJ_Script>().SetDamage();
                    }
                }
                else
                {
                    Debug.DrawRay(m_MuzzleTransform.position, vShotDis * m_fRayDistance, Color.green, 0.3f);
                }
            }
        }

        else
        {
            float fRandomX = Random.Range(-m_fArms_Accuracy, m_fArms_Accuracy);
            float fRandomY = Random.Range(-m_fArms_Accuracy, m_fArms_Accuracy);
            float fRandomZ = Random.Range(-m_fArms_Accuracy, m_fArms_Accuracy);
            Vector3 vShotDis = m_Camera.transform.forward + new Vector3(fRandomX, fRandomY, fRandomZ);

            if (Physics.Raycast(m_MuzzleTransform.position, vShotDis, out hit, m_fRayDistance, LayerMask.GetMask("Enemy", "Background", "Explosive", "StaticInteractOBJ")))
            {
                Debug.DrawRay(m_MuzzleTransform.position, vShotDis * hit.distance, Color.green, 0.3f);
                Debug.DrawRay(hit.point, m_Camera.transform.forward * (m_fRayDistance - hit.distance), Color.red, 0.3f);

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyDamageReceiver damageReceiver;
                    hit.transform.TryGetComponent(out damageReceiver);
                    damageReceiver.GiveDamage(GetDamage());

                    Instantiate(m_BloodSprayEffect, hit.point, hit.transform.rotation);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                {
                    Instantiate(m_GunExplosiveEffect, hit.point, hit.transform.rotation);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Explosive"))
                {
                    Instantiate(m_GunExplosiveEffect, hit.point, hit.transform.rotation);
                    hit.transform.gameObject.GetComponent<ExplosiveOBJ_Script>().SetDamage();
                }
            }

            else
            {
                Debug.DrawRay(m_MuzzleTransform.position, vShotDis * m_fRayDistance, Color.green, 0.3f);
            }
        }

        Instantiate(m_MuzzleFlashEffect, m_MuzzleTransform.position, m_MuzzleTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
        Instantiate(m_CartridgeEjectEffect, m_weaponTransform.position, m_weaponTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
    }

    public void Trans_Arms_Accuracy(float _TransValue)
    {
        m_fArms_Accuracy += _TransValue;
    }

    public override void Set_Arms_Accuracy(bool _Aim, float _Value)
    {
        m_bAim = _Aim;
        m_fArms_Accuracy = _Value;
        m_fPre_Accuracy = m_fArms_Accuracy;
    }

    //public LaserPointer GetLaserPointer()
    //{
    //    return m_LaserPointer;
    //}

    //public void SetLaserPointer(LaserPointer _Laserpointer)
    //{
    //    m_LaserPointer = _Laserpointer;
    //}

    //public Transform GetMuzzleTransform()
    //{
    //    return m_MuzzleTransform;
    //}

    //public void SetMuzzleTransform(Transform _MuzzleTrans)
    //{
    //    m_MuzzleTransform = _MuzzleTrans;
    //}
}