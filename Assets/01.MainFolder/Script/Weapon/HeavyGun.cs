using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeavyGun : MonoBehaviour
{
    [System.Serializable]
    public struct HeavyGunInfo
    {
        public float _fRatio;
        public float _fRangeDis;
        public int _iAmmo;
        public int _iAtk;
    }

    public Camera m_MainCamrea;
    public HeavyGunInfo m_Info;
    public GameObject m_Move_Part; // Y축 돌아갈 오브젝트
    public GameObject m_HeavyGun; // X축 돌아갈 총기 오브젝트
    public GameObject m_PlayerSpace; // 플레이어가 들어갈 자리
    public GameObject m_Muzzle;
    public LaserPointer m_Laserpointer;
    public Camera m_HeavyCam;
    public GameObject m_CartridgeSpace;

    public GameObject m_BloodSprayEffect;
    public GameObject m_GunExplosiveEffect;
    public GameObject m_MuzzleFlashEffect;
    public GameObject m_CartridgeEjectEffect;
    private SoundScript m_SoundScript;

    public PlayerStatus m_Player { get; set; }

    private bool m_bRiding = false;
    private float m_fRotX;
    private float m_fRotY;
    private float m_fCoolTime2Fire;
    private float m_fAccTime;

    // Start is called before the first frame update
    void Awake()
    {
        m_SoundScript = GetComponent<SoundScript>();
        m_HeavyCam.enabled = false;
        m_Player = GameObject.Find("Player").GetComponent<PlayerStatus>();

        m_fRotX = m_HeavyGun.transform.rotation.eulerAngles.x;
        m_fRotY = m_Move_Part.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        GetOff();

        if (m_bRiding)
        {
            UpdateTimeVariables();
            Player_Position_Hold();
            Rotate_MovePart_and_Gun();
            Fire_Gun();
            Shot_Shake();
        }

        else
        {

        }
    }

    private void GetOff()
    {
        if (m_bRiding && Input.GetKeyDown(KeyCode.E))
        {
            m_Player.TPScameraON();
            m_Player.GetItem().SetVisible(true, true);
            m_Player.SetAnimatorParameterBool("HeavyGrip", false);
            m_bRiding = false;
            m_Move_Part.transform.rotation = Quaternion.Euler(0, 0, 0);
            m_HeavyGun.transform.rotation = Quaternion.Euler(0, 0, 0);
            m_HeavyCam.enabled = false;
            m_HeavyCam.GetComponent<AudioListener>().enabled = false;
            m_MainCamrea.enabled = true;
            m_MainCamrea.GetComponent<AudioListener>().enabled = true;
        }
    }

    public void Player_RideTo_HeavyGun()
    {

        m_MainCamrea.enabled = false;
        m_MainCamrea.GetComponent<AudioListener>().enabled = false;
        m_HeavyCam.enabled = true;
        m_HeavyCam.GetComponent<AudioListener>().enabled = true;
        m_bRiding = true;
        m_Player.GetItem().SetVisible(false, true);
        m_Player.SetAnimatorParameterBool("HeavyGrip", true);
        if(ExtractionQuestManager.GetInstance().GetMission() == ExtractionQuestManager.Mission.Use_MiniGun)
        {
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Use_MiniGun);
        }
    }

    private void Player_Position_Hold()
    {
        m_Player.transform.position = m_PlayerSpace.transform.position;
    }

    private void Rotate_MovePart_and_Gun()
    {
        m_fRotX += Input.GetAxis("Mouse Y") * 50f * Time.deltaTime;
        m_fRotY += Input.GetAxis("Mouse X") * 50f * Time.deltaTime;

        m_fRotX = Mathf.Clamp(m_fRotX, -15f, 15f);
        m_fRotY = Mathf.Clamp(m_fRotY, -45f, 45f);

        Quaternion RotY = Quaternion.Euler(0, m_fRotY /*+ 180f*/, 0);
        Quaternion RotX = Quaternion.Euler(m_fRotX, m_fRotY /*+ 180f*/, 0);

        m_Move_Part.transform.rotation = RotY;
        m_HeavyGun.transform.rotation = RotX;
        m_CartridgeSpace.transform.rotation = RotX;
    }

    private void UpdateTimeVariables()
    {
        m_fCoolTime2Fire -= Time.deltaTime;
        if (m_fCoolTime2Fire < 0f)
        {
            m_fCoolTime2Fire = 0f;
        }
    }

    public void Shot_Shake()
    {
        m_fAccTime += Time.deltaTime;

        if (m_fAccTime < Mathf.PI * 0.05f)
            m_HeavyGun.transform.rotation *= Quaternion.Euler(Mathf.Sin(2f * m_fAccTime) * 5f, 0, 0);
    }

    private void Fire_Gun()
    {
        float fRandomX = UnityEngine.Random.Range(-0.01f, 0.01f);
        float fRandomY = UnityEngine.Random.Range(-0.01f, 0.01f);
        float fRandomZ = UnityEngine.Random.Range(-0.01f, 0.01f);
        Vector3 vShotDis = m_HeavyCam.transform.forward + new Vector3(fRandomX, fRandomY, fRandomZ);

        if (Input.GetButton("Fire") && m_Info._iAmmo > 0)
        {
            if (m_fCoolTime2Fire == 0f)
            {
                m_SoundScript.Play_JustOne(0);
                m_Info._iAmmo -= 1;

                RaycastHit[] hit;
                hit = Physics.RaycastAll(m_Muzzle.transform.position, vShotDis, m_Info._fRangeDis, 1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Background") | 1 << LayerMask.NameToLayer("Explosive"));

                int N = hit.Length;
                if (hit.Length >= 1)
                {
                    //가장 작은 hit.distance를 가진 세놈의 index를 구해요
                    for (int i = 0; i < hit.Length; i++)
                    {
                        for (int j = i + 1; j < hit.Length; j++)
                        {
                            if (hit[i].distance > hit[j].distance)
                            {
                                RaycastHit temp = hit[i];
                                hit[i] = hit[j];
                                hit[j] = temp;
                            }
                        }
                    }

                    for (int i = 0; i < ((hit.Length > 3) ? 3 : hit.Length); ++i)
                    {
                        if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        {
                            EnemyDamageReceiver damageReceiver;
                            hit[i].transform.TryGetComponent(out damageReceiver);
                            damageReceiver.GiveDamage(m_Info._iAtk);

                            Instantiate(m_BloodSprayEffect, hit[i].point, hit[i].transform.rotation);
                        }
                        else if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                        {
                            Instantiate(m_GunExplosiveEffect, hit[i].point, hit[i].transform.rotation);
                            break;
                        }
                        else if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Explosive"))
                        {
                            Instantiate(m_GunExplosiveEffect, hit[i].point, hit[i].transform.rotation);
                            hit[i].transform.gameObject.GetComponent<ExplosiveOBJ_Script>().SetDamage();
                            break;
                        }
                    }

                    Debug.DrawRay(m_Muzzle.transform.position, vShotDis * hit[0].distance, Color.green, 0.3f);
                    Debug.DrawRay(hit[0].point, vShotDis * (m_Info._fRangeDis - hit[0].distance), Color.red, 0.3f);
                }

                m_fAccTime = 0f;
                m_fCoolTime2Fire = m_Info._fRatio;

                GameObject flashEffect = Instantiate(m_MuzzleFlashEffect, m_Muzzle.transform.position, m_Muzzle.transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
                flashEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Instantiate(m_CartridgeEjectEffect, m_CartridgeSpace.transform.position, m_CartridgeSpace.transform.rotation * Quaternion.Euler(new Vector3(0, -90, 0)));
            }

            else
            {
                Debug.DrawRay(m_Muzzle.transform.position, vShotDis * m_Info._fRangeDis, Color.green, 0.3f);
            }
        }

        else
        {
            return;
        }
    }
}
