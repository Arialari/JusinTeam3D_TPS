using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public enum Item { Primary, Secondary, Throwing, Healing, ItemEnd }
    // Components
    private Animator m_Animator;

    //UI
    private TPSCharacterController m_TPSCharacterController;

    //Sound
    public SoundScript m_SoundScript { get; set; }
    private int m_iSoundCount = 1;

    //Weapon
    public uint[] m_iTotalAmmo { get; set; } = new uint[(int)Weapon.Type.End] { 90, 30, 20, 120, 99 };
    //Weapon Var
    private Item m_eSelectedItem = Item.Primary;
    private Weapon m_PrimaryWeapon;
    private Weapon m_SecondaryWeapon;
    private ThrowWeapon m_ThrowWeapon;
    private Medicine m_Medicine;
    private GameObject[] m_Item = new GameObject[(int)Item.ItemEnd];
    private GameObject m_PrefabThrowWeapon;
    private Transform m_RightArmTransform;

    public GameObject m_FlashLight;

    public int m_iAmmo_Throwable;
    public int m_iAmmo_Medicine;

    public int _MaxHP = 0;
    public int _HP = 0;
    public int _TemporaryHP_Under = 0;
    public int _TemporaryHP_Upper = 0;
    public int _TotalHP = 0;

    public int m_iTemporaryHP_Point = 0;

    public int m_iPocketMoney = 0;

    [HideInInspector] public bool _bUsePainPhill = false;
    [HideInInspector] public bool _TemporaryHP_UnderON = false;
    [HideInInspector] public bool _TemporaryHP_UpperON = false;

    [HideInInspector] public int m_iPrimaryWeaponNUM;
    [HideInInspector] public int m_iSecondaryWeaponNUM;
    [HideInInspector] public int m_iMedicineNUM;
    [HideInInspector] public int m_iThrowableNUM;
    [HideInInspector] public bool m_bChangePrimaryWeap = false; // 장착된 주무기 바뀔 때 쓰는 변수.
    [HideInInspector] public bool m_bChangeSecondaryWeap = false; // 장착된 보조무기 바뀔 때 쓰는 변수.
    [HideInInspector] public bool m_bChangeThrowable = false; // 장착된 투척물 바뀔 때 쓰는 변수.
    [HideInInspector] public bool m_bChangeMedicine = false; // 장착된 의료품 바뀔 때 쓴느 변수.

    public int _MonsterATT = 0;

    float m_fAccTime;
    public int _iUsePainPhillCount = 0;

    public GameObject _CamRayTarget;
    public bool _bRayCollToUseItem;
    Camera_Ray _cameraRay;
    private float _fRangeToPlayer;
    private float _fDis_PlayerAndOBJ;
    private bool m_bFlash = false;
    public bool m_bShot = false;

    private InGameUIMgr _gameUIManager;
    public MainMenuUI _mainMenuUI;

    private Quaternion m_qRotation;

    public GameObject m_BloodSprayEffect;

    private HeavyGun m_HeavyGun;
    public GameObject m_OBJFollow;

    public CoinScript m_COIN_Script;
    private MainUI_Head m_MainUI_Head;

    private GameObject m_Heli;
    //private Throwable m_Throwable;
    //private HealingItem m_HealingItem;

    // Start is called before the first frame update
    void Start()
    {
        Transform childTransform = transform.GetChild(0);
        childTransform.TryGetComponent(out m_Animator);
        SpawnItems();

        m_iAmmo_Throwable = 5;
        m_iAmmo_Medicine = 3;

        _MaxHP = 100;
        _HP = _MaxHP;
        _TemporaryHP_Under = _HP;
        _fRangeToPlayer = 4.0f;

        m_iPocketMoney = 1000;

        _cameraRay = Camera.main.GetComponent<Camera_Ray>();

        _gameUIManager = GameObject.Find("GameUI Manager").GetComponent<InGameUIMgr>();
        _mainMenuUI = GameObject.Find("Main Menu Panel").GetComponent<MainMenuUI>();
        GameObject.Find("MainCam").TryGetComponent(out m_TPSCharacterController);
        m_SoundScript = GetComponent<SoundScript>();

        m_FlashLight.GetComponent<Light>().enabled = false;
        m_MainUI_Head = GameObject.Find("Canvas").GetComponent<MainUI_Head>();

        m_Heli = GameObject.Find("Heli1");
    }

    // Update is called once per frame
    void Update()
    {
        m_fAccTime += Time.deltaTime;

        _mainMenuUI.Update();

        ChangeTo_BuyItem();

        if (m_iPocketMoney < 0)
        {
            m_iPocketMoney = 0;
        }

        _CamRayTarget = _cameraRay._RayHitObject;

        if (null != _CamRayTarget)
        {
            PopUpToggle();
        }

        if (!m_Animator.GetBool("HeavyGrip"))
        {
            Using_OBJ();
            KeyInput();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SetDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            _HP += 10;
            _TemporaryHP_Under += 10;
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            _bUsePainPhill = true;
            _iUsePainPhillCount = 1;
        }

        if (_HP > _MaxHP)
        {
            _HP -= _HP - _MaxHP;
        }

        if (_HP < 0)
        {
            _HP -= _HP;
        }

        TemporaryHP_Logic();

        if (null != _CamRayTarget)
        {
            _fDis_PlayerAndOBJ = Vector3.Distance(transform.position, _CamRayTarget.transform.position);
            if(_CamRayTarget == m_Heli)
            {
                _fDis_PlayerAndOBJ -= 10f;
            }
        }
        else
        {
            _CamRayTarget = null;
            _fDis_PlayerAndOBJ = _fRangeToPlayer + 0.1f;
        }

        for (int i = 0; i < (int)Weapon.Type.End; ++i)
        {
            if (m_iTotalAmmo[i] > 999)
            {
                m_iTotalAmmo[i] = 999;
            }
        }

        if (m_iPocketMoney > 9999)
        {
            m_iPocketMoney = 9999;
        }

        if (m_iAmmo_Medicine <= 0)
        {
            UseMedicine();
        }

        //Debug.Log(_HP.ToString());
        //Debug.Log(_TemporaryHP_Under.ToString());
        //Debug.Log(_TemporaryHP_Upper.ToString());
    }
    private void KeyInput()
    {
        if (m_TPSCharacterController._bOpenUI)
        {
            return;
        }

        if (!m_Animator.GetBool("Reload")) // 애니메이터 Reload 파라미터가 false일 경우
        {
            if (Input.GetButtonDown("1") && m_eSelectedItem != Item.Primary)
            {
                if (m_eSelectedItem == Item.Throwing)
                {
                    Destroy(m_Item[(int)Item.Throwing]);
                }

                else
                {
                    m_Item[(int)m_eSelectedItem].SetVisible(false, true);
                }

                SetAnimatorParameterBool("HasMelee", false);
                m_eSelectedItem = Item.Primary;
                m_Item[(int)m_eSelectedItem].SetVisible(true, true);
            }

            if (Input.GetButtonDown("2") && m_eSelectedItem != Item.Secondary)
            {
                if (m_eSelectedItem == Item.Throwing)
                {
                    Destroy(m_Item[(int)Item.Throwing]);
                }

                else
                {
                    m_Item[(int)m_eSelectedItem].SetVisible(false, true);
                }

                m_eSelectedItem = Item.Secondary;
                m_Item[(int)m_eSelectedItem].SetVisible(true, true);

                if (m_SecondaryWeapon.GetIsMelee())
                {
                    SetAnimatorParameterBool("HasMelee", true);
                }
            }

            if (Input.GetButtonDown("3") && m_eSelectedItem != Item.Throwing && m_iAmmo_Throwable > 0)
            {
                SetAnimatorParameterBool("HasMelee", false);
                m_Item[(int)m_eSelectedItem].SetVisible(false, true);
                SpawnThrowItem();
                m_eSelectedItem = Item.Throwing;
            }

            if (Input.GetButtonDown("4") && m_eSelectedItem != Item.Healing && m_iAmmo_Medicine > 0)
            {
                if (m_eSelectedItem == Item.Throwing)
                {
                    Destroy(m_Item[(int)Item.Throwing]);
                }

                else
                {
                    m_Item[(int)m_eSelectedItem].SetVisible(false, true);
                }

                SetAnimatorParameterBool("HasMelee", false);
                m_eSelectedItem = Item.Healing;
                m_Item[(int)m_eSelectedItem].SetVisible(true, true);
            }

            if (!m_Animator.GetBool("Run")) // 애니메이터 Run 파라미터가 false일 경우
            {
                if (Input.GetButton("Fire")) // 발사 누르고 있을 경우
                {
                    switch (m_eSelectedItem)
                    {
                        case Item.Primary:
                            m_PrimaryWeapon.Fire();
                            break;
                        case Item.Secondary:
                            m_SecondaryWeapon.Fire();
                            break;
                        case Item.Throwing:
                            m_ThrowWeapon.SetPinOff(true);
                            break;
                        case Item.Healing:

                            if (m_Medicine.Get_UsingCount() == 1)
                            {
                                if (m_iSoundCount == 1)
                                {
                                    m_SoundScript.Play_JustOne(5);
                                }

                                m_iSoundCount = 0;
                                _gameUIManager.m_Using_GageBar.Set_UsingTime(m_Medicine.Get_UsingTime());
                                _gameUIManager.m_Using_GageBar.Set_UsingObjName(m_Medicine.Get_MedicineName());
                                _gameUIManager.m_Using_GageBar.Filling_Gage();
                                m_Medicine.ChangeTo_Parameter_ofAnimator(m_Animator, true);
                            }
                            else
                            {
                                m_SoundScript.m_AudioSrc.Stop();
                                _gameUIManager.m_Using_GageBar.Stop_Filling();
                                m_Medicine.ChangeTo_Parameter_ofAnimator(m_Animator, false);
                            }

                            m_Medicine.Set_bUsing(true);
                            break;
                    }
                }

                if (Input.GetButtonUp("Fire")) // 발사 뗄 경우
                {
                    switch (m_eSelectedItem)
                    {
                        case Item.Primary:
                            m_PrimaryWeapon.StopFire();
                            break;
                        case Item.Secondary:
                            m_SecondaryWeapon.StopFire();
                            break;
                        case Item.Throwing:
                            SetAnimatorParameterTrigger("Throw");
                            break;
                        case Item.Healing:
                            m_SoundScript.m_AudioSrc.Stop();
                            m_iSoundCount = 1;
                            _gameUIManager.m_Using_GageBar.Stop_Filling();
                            m_Medicine.ChangeTo_Parameter_ofAnimator(m_Animator, false);
                            m_Medicine.StopUsing();
                            break;
                    }
                }

                if (Input.GetButton("Aim")) // 에이밍 누르고있을 경우
                {
                    switch (m_eSelectedItem)
                    {
                        case Item.Primary:
                            
                            if (m_PrimaryWeapon.m_WeaponInfo.eType == Weapon.Type.SR)
                            {
                                //m_PrimaryWeapon.m_SoundScript.Play_JustOne(3);

                                if (Input.GetKey(KeyCode.LeftShift) && !m_bShot)
                                { 
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        m_bShot = true;
                                        m_Animator.speed = 1f;
                                        break;
                                    }
                                    m_Animator.speed = 0.0f;
                                }
                                if (Input.GetKeyUp(KeyCode.LeftShift))
                                    m_Animator.speed = 1f;
                            }

                            else
                            {
                                m_PrimaryWeapon.Set_Acc(true, 0.01f);
                                m_PrimaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(true);
                            }
                            
                            break;
                        case Item.Secondary:
                            if (m_SecondaryWeapon.GetLaserPointer() == null)
                                break;
                            else
                                m_SecondaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(true);
                            break;
                        case Item.Throwing:
                            break;
                        case Item.Healing:
                            break;
                        default:
                            break;
                    }
                }

                if (Input.GetButtonUp("Aim")) // 에이밍 뗄 경우
                {
                    switch (m_eSelectedItem)
                    {
                        case Item.Primary:
                            if (m_PrimaryWeapon.m_WeaponInfo.eType == Weapon.Type.SR)
                            {
                                m_PrimaryWeapon.m_SoundScript.Play_JustOne(3);
                                m_Animator.speed = 1f;
                                break;
                            }
                            m_PrimaryWeapon.Set_Acc(false, 0.05f);
                            m_PrimaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(false);
                            break;
                        case Item.Secondary:
                            if (m_SecondaryWeapon.GetLaserPointer() == null)
                                break;
                            else
                                m_SecondaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(false);
                            break;
                        case Item.Throwing:
                            break;
                        case Item.Healing:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        else // Reload 파라미터가 true 일 경우
        {
            if (m_PrimaryWeapon.m_WeaponInfo.eType != Weapon.Type.SR)
                m_PrimaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(false);

            if (m_SecondaryWeapon.GetLaserPointer() != null)
                m_SecondaryWeapon.GetLaserPointer().GetComponent<LaserPointer>().Laser_OnOff(false);
        }

        if (Input.GetButtonDown("Reload")) // 재장전 누를 경우
        {
            switch (m_eSelectedItem)
            {
                case Item.Primary:
                    if (m_PrimaryWeapon.GetAmmo() < m_PrimaryWeapon.GetMag() &&
                        m_PrimaryWeapon.GetTotalAmmo() > 0)
                    {
                        m_PrimaryWeapon.m_SoundScript.Play_JustOne(1);
                        SetAnimatorParameterBool("Reload", true);
                    }
                    break;
                case Item.Secondary:
                    if (m_SecondaryWeapon.GetAmmo() < m_SecondaryWeapon.GetMag() &&
                        m_SecondaryWeapon.GetTotalAmmo() > 0)
                    {
                        m_SecondaryWeapon.m_SoundScript.Play_JustOne(1);
                        SetAnimatorParameterBool("Reload", true);
                    }
                    break;
                case Item.Throwing:
                    break;
                case Item.Healing:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) // 플래시 라이트
        {
            m_PrimaryWeapon.m_SoundScript.Play_JustOne(2);
            m_FlashLight.GetComponent<Light>().enabled = !m_FlashLight.GetComponent<Light>().enabled;
        }
    }

    public void ThrowBOMB()
    {
        m_ThrowWeapon.Throw();
        m_ThrowWeapon = null;
        --m_iAmmo_Throwable;
        if (m_iAmmo_Throwable > 0)
        {
            SpawnThrowItem();
        }
        else
        {
            m_eSelectedItem = Item.Primary;
            m_Item[(int)m_eSelectedItem].SetVisible(true, true);
        }
    }

    public void UseMedicine()
    {
        if (m_eSelectedItem == Item.Healing)
        {
            if (m_iAmmo_Medicine <= 0)
            {
                m_Item[(int)m_eSelectedItem].SetVisible(false, true);
                SetAnimatorParameterBool("UseOBJ", false);
                SetAnimatorParameterBool("PainPill", false);
                m_eSelectedItem = Item.Primary;
                m_Item[(int)m_eSelectedItem].SetVisible(true, true);
            } 
        }
    }

    public void SlashEnd()
    {
        transform.localRotation = m_qRotation;
    }

    public void FlashLight()
    {
        m_bFlash = !m_bFlash;
        m_PrimaryWeapon.FlashLight_OnOff(m_bFlash);
    }

    public Weapon GetPrimaryWeapon()
    {
        return m_PrimaryWeapon;
    }
    public Weapon GetSecondaryWeapon()
    {
        return m_SecondaryWeapon;
    }
    public Item GetSelectedItem()
    {
        return m_eSelectedItem;
    }

    public GameObject GetItem()
    {
        return m_Item[(int)m_eSelectedItem];
    }

    public Animator GetAnimator()
    {
        return m_Animator;
    }

    public void TPScameraON()
    {
        m_TPSCharacterController.CamOn();
    }

    public void Reloaded()
    {
        switch (m_eSelectedItem)
        {
            case Item.Primary:
                m_PrimaryWeapon.Reload();
                break;
            case Item.Secondary:
                m_SecondaryWeapon.Reload();
                break;
            case Item.Throwing:
                break;
            case Item.Healing:
                break;
        }
    }

    public void SetDamage(int _iMonAtt)
    {
        if (_TemporaryHP_Under <= _HP && _TemporaryHP_Upper <= 0)
            _HP -= _iMonAtt;

        else if (_TemporaryHP_Under >= _HP && _TemporaryHP_Upper >= 1)
        {
            _TemporaryHP_Upper -= _iMonAtt;
        }

        else if (_TemporaryHP_Under >= 100 && _TemporaryHP_Upper >= 1)
        {
            _TemporaryHP_Upper -= _iMonAtt;
        }

        else if (_TemporaryHP_Under >= _HP && _TemporaryHP_Upper <= 0)
        {
            _TemporaryHP_Under -= _iMonAtt;
        }
    }

    void TemporaryHP_Logic()
    {
        if (_TemporaryHP_Under > 100)
            _TemporaryHP_Under = 100;

        if (_TemporaryHP_Upper > 100)
            _TemporaryHP_Upper = 100;

        if (_TemporaryHP_Under < 0)
            _TemporaryHP_Under = 0;

        if (_TemporaryHP_Upper < 0)
            _TemporaryHP_Upper = 0;

        if (_bUsePainPhill)
        {
            if (_iUsePainPhillCount == 1)
            {
                if (_HP >= 100) // 100 이상
                {
                    if (_TemporaryHP_Upper <= 0)
                        _TemporaryHP_Upper = m_iTemporaryHP_Point;

                    else if (_TemporaryHP_Upper > 0)
                        _TemporaryHP_Upper += m_iTemporaryHP_Point;
                }

                else if (_HP < 100 && _HP > 70) // 71 ~ 99
                {
                    if (_TemporaryHP_Upper <= 0 && _TemporaryHP_Under <= _HP)
                    {
                        _TemporaryHP_Under += 100 - _HP;
                        _TemporaryHP_Upper += _HP + m_iTemporaryHP_Point - 100;
                    }

                    else if (_TemporaryHP_Under > _HP && _TemporaryHP_Upper > 0)
                    {
                        _TemporaryHP_Under += 100 - _HP;

                        if (_TemporaryHP_Under >= 100)
                            _TemporaryHP_Upper += m_iTemporaryHP_Point;
                    }
                }

                else if (_HP < 71 && _HP > 0) // 1 ~ 70
                {
                    if (_TemporaryHP_Upper <= 0 && _TemporaryHP_Under >= _HP)
                    {
                        int i = 0;

                        if (_TemporaryHP_Under < 100)
                        {
                            i = 100 - _TemporaryHP_Under;
                            _TemporaryHP_Under += m_iTemporaryHP_Point;
                        }

                        if (_TemporaryHP_Upper < 100 && _TemporaryHP_Under >= 100)
                            _TemporaryHP_Upper += m_iTemporaryHP_Point - i;
                    }

                    else if (_TemporaryHP_Under >= _HP && _TemporaryHP_Upper > 0)
                    {
                        if (_TemporaryHP_Upper < 100 && _TemporaryHP_Under >= 100)
                            _TemporaryHP_Upper += m_iTemporaryHP_Point;
                    }
                }

                _iUsePainPhillCount = 0;
            }

            // 체력이 100이고 (상)임시체력이 1 이상일 때.
            if (_HP >= 100 &&
                _TemporaryHP_Upper >= 1)
            {
                // 1초마다 (상)임시체력이 1씩 깎인다.
                if (m_fAccTime >= 1.0f)
                {
                    _TemporaryHP_Upper -= 1;
                    m_fAccTime = 0.0f;
                }
            }

            // (상)임시체력이 0 이하일 때.
            else if (_TemporaryHP_Upper <= 0 && _HP >= 100)
            {
                m_fAccTime = 0.0f;
                _bUsePainPhill = false;
            }

            // (상)임시체력이 1 이상이고 (하)임시체력이 체력보다 크거나,
            // (상)임시체력이 0 이하이고 (하)임시체력이 체력보다 클 때.
            if ((_TemporaryHP_Upper >= 1 && _TemporaryHP_Under > _HP) ||
                (_TemporaryHP_Upper <= 0 && _TemporaryHP_Under > _HP))
            {
                if (m_fAccTime >= 0.5f)
                {
                    if (_TemporaryHP_Upper >= 1 && _TemporaryHP_Under > _HP)
                        _TemporaryHP_Upper -= 1;

                    else if (_TemporaryHP_Upper <= 0 && _TemporaryHP_Under > _HP)
                        _TemporaryHP_Under -= 1;

                    m_fAccTime = 0.0f;
                }
            }

            else if (_TemporaryHP_Upper <= 0 && _TemporaryHP_Under <= _HP)
            {
                m_fAccTime = 0.0f;
                _bUsePainPhill = false;
            }
        }

        else if (!_bUsePainPhill)
        {
            _TemporaryHP_Under = _HP;
            _TemporaryHP_Upper = 0;
        }

        if (_TemporaryHP_Under > _HP)
            _TemporaryHP_UnderON = true;
        else
            _TemporaryHP_UnderON = false;

        if (_TemporaryHP_Upper > 0)
            _TemporaryHP_UpperON = true;
        else
            _TemporaryHP_UpperON = false;
    }

    public void GiveDamage(float _fDamage)
    {
        SetDamage((int)_fDamage);
    }
    private void SpawnItems()
    {
        GameObject PrimaryWeapon = WeaponList.m_tWeapInfo[1]._Prefab;
        GameObject SecondaryWeapon = WeaponList.m_tWeapInfo[3]._Prefab;
        GameObject HealingItem = WeaponList.m_tItemInfo[3]._Prefab;
        m_PrefabThrowWeapon = Resources.Load("Throwable Grenade") as GameObject;
        m_RightArmTransform = transform.GetTransformInAllChildren("RightSocket");

        m_Item[(int)Item.Primary] = Instantiate(PrimaryWeapon, m_RightArmTransform);
        m_Item[(int)Item.Primary].transform.localScale = new Vector3(40f, 40f, 40f);
        
        m_Item[(int)Item.Secondary] = Instantiate(SecondaryWeapon, m_RightArmTransform);
        m_Item[(int)Item.Secondary].transform.localScale = new Vector3(40f, 40f, 40f);
        m_Item[(int)Item.Secondary].SetVisible(false, true);
        
        m_Item[(int)Item.Healing] = Instantiate(HealingItem, m_RightArmTransform);
        m_Item[(int)Item.Healing].transform.localScale = new Vector3(40f, 40f, 40f);
        m_Item[(int)Item.Healing].SetVisible(false, true);
        m_iMedicineNUM = 3;

        m_PrimaryWeapon = m_Item[(int)Item.Primary].GetComponent<Weapon>();
        m_SecondaryWeapon = m_Item[(int)Item.Secondary].GetComponent<Weapon>();
        m_Medicine = m_Item[(int)Item.Healing].GetComponent<IMedicineScript>().GetMedicine();
        //m_ThrowWeapon = m_Item[(int)Item.Throwing].GetComponent<ThrowWeapon>();
    }
    private void ChangeTo_BuyItem() // 상점에서 아이템 구매 할 때 교체기능
    {
        if (m_bChangePrimaryWeap)
        {
            m_Item[(int)Item.Primary].SetVisible(true, true);
            m_PrimaryWeapon.GetComponent<BoxCollider>().enabled = true;
            m_PrimaryWeapon.GetComponent<Rigidbody>().isKinematic = false;
            m_PrimaryWeapon.m_bDropWeap = true;
            m_Item[(int)Item.Primary].transform.parent = null;

            GameObject PrimaryWeapon = WeaponList.m_tWeapInfo[m_iPrimaryWeaponNUM]._Prefab;

            if (m_eSelectedItem == Item.Secondary ||
                m_eSelectedItem == Item.Throwing ||
                m_eSelectedItem == Item.Healing)
                PrimaryWeapon.SetVisible(false, true);
            else
                PrimaryWeapon.SetVisible(true, true);

            m_Item[(int)Item.Primary] = Instantiate(PrimaryWeapon, m_RightArmTransform);
            m_Item[(int)Item.Primary].transform.localScale = new Vector3(40f, 40f, 40f);
            m_PrimaryWeapon = m_Item[(int)Item.Primary].GetComponent<Weapon>();

            m_bChangePrimaryWeap = false;
        }

        if (m_bChangeSecondaryWeap)
        {
            m_Item[(int)Item.Secondary].SetVisible(true, true);
            m_SecondaryWeapon.GetComponent<BoxCollider>().enabled = true;
            m_SecondaryWeapon.GetComponent<Rigidbody>().isKinematic = false;
            m_SecondaryWeapon.m_bDropWeap = true;
            m_Item[(int)Item.Secondary].transform.parent = null;

            GameObject SecondaryWeapon = WeaponList.m_tWeapInfo[m_iSecondaryWeaponNUM]._Prefab;

            if (m_eSelectedItem == Item.Primary ||
                m_eSelectedItem == Item.Throwing ||
                m_eSelectedItem == Item.Healing)
                SecondaryWeapon.SetVisible(false, true);
            else
                SecondaryWeapon.SetVisible(true, true);

            m_Item[(int)Item.Secondary] = Instantiate(SecondaryWeapon, m_RightArmTransform);
            m_Item[(int)Item.Secondary].transform.localScale = new Vector3(40f, 40f, 40f);
            m_SecondaryWeapon = m_Item[(int)Item.Secondary].GetComponent<Weapon>();
            
            m_bChangeSecondaryWeap = false;
        }

        if (m_bChangeThrowable)
        {
            //SpawnThrowItem();
            //m_Item[(int)Item.Throwing].SetVisible(true, true);
            //m_Item[(int)Item.Throwing].GetComponent<CapsuleCollider>().enabled = true;
            //m_Item[(int)Item.Throwing].GetComponent<Rigidbody>().isKinematic = false;
            //m_Item[(int)Item.Throwing].transform.parent = null;

            m_PrefabThrowWeapon = WeaponList.m_tItemInfo[m_iThrowableNUM]._Prefab;

            if(m_eSelectedItem == Item.Throwing)
            {
                SpawnThrowItem();
                m_Item[(int)Item.Throwing].SetVisible(true, true);
            }
            //else
            //{
            //    m_Item[(int)Item.Throwing].SetVisible(false, true);
            //}

            m_bChangeThrowable = false;
        }

        if (m_bChangeMedicine)
        {
            m_Item[(int)Item.Healing].SetVisible(true, true);
            m_Item[(int)Item.Healing].GetComponent<BoxCollider>().enabled = true;
            m_Item[(int)Item.Healing].GetComponent<Rigidbody>().isKinematic = false;
            m_Item[(int)Item.Healing].transform.parent = null;

            GameObject MedicinePrefab = WeaponList.m_tItemInfo[m_iMedicineNUM]._Prefab;

            if (m_eSelectedItem == Item.Healing)
            {
                MedicinePrefab.SetVisible(true, true);
            }
            else
            {
                MedicinePrefab.SetVisible(false, true);
            }

            m_Item[(int)Item.Healing] = Instantiate(MedicinePrefab, m_RightArmTransform);
            m_Item[(int)Item.Healing].transform.localScale = new Vector3(40f, 40f, 40f);
            m_Medicine = m_Item[(int)Item.Healing].GetComponent<IMedicineScript>().GetMedicine();

            m_bChangeMedicine = false;
        }
    }
    private void ChangeTo_GetDropItem() // 드랍되어 있는 아이템을 주울 때 교체기능
    {
        switch (_CamRayTarget.GetComponent<Weapon>().Get_ItemType())
        {
            case Item.Primary:
                m_Item[(int)Item.Primary].SetVisible(true, true);
                m_PrimaryWeapon.GetComponent<BoxCollider>().enabled = true;
                m_PrimaryWeapon.GetComponent<Rigidbody>().isKinematic = false;
                m_PrimaryWeapon.m_bDropWeap = true;
                m_Item[(int)Item.Primary].transform.parent = null;

                m_PrimaryWeapon = _CamRayTarget.GetComponent<Weapon>();
                _CamRayTarget.GetComponent<Weapon>().m_bDropWeap = false;
                _CamRayTarget.GetComponent<BoxCollider>().enabled = false;
                _CamRayTarget.GetComponent<Rigidbody>().isKinematic = true;
                _CamRayTarget.transform.parent = m_RightArmTransform;
                _CamRayTarget.transform.position = m_RightArmTransform.position;
                _CamRayTarget.transform.rotation = m_RightArmTransform.rotation;
                _gameUIManager.m_TabImg_PrimaryWeap.sprite = _CamRayTarget.GetComponent<Weapon>().m_ImgWeaponIcon;
                _gameUIManager.m_PrimaryAmmoIcon.sprite = _CamRayTarget.GetComponent<Weapon>().m_ImgAmmoIcon;

                if (m_eSelectedItem == Item.Secondary)
                    _CamRayTarget.SetVisible(false, true);
                else
                    _CamRayTarget.SetVisible(true, true);
                break;

            case Item.Secondary:
                m_Item[(int)Item.Secondary].SetVisible(true, true);
                m_SecondaryWeapon.GetComponent<BoxCollider>().enabled = true;
                m_SecondaryWeapon.GetComponent<Rigidbody>().isKinematic = false;
                m_SecondaryWeapon.m_bDropWeap = true;
                m_Item[(int)Item.Secondary].transform.parent = null;
                
                m_SecondaryWeapon = _CamRayTarget.GetComponent<Weapon>();
                _CamRayTarget.GetComponent<Weapon>().m_bDropWeap = false;
                _CamRayTarget.GetComponent<BoxCollider>().enabled = false;
                _CamRayTarget.GetComponent<Rigidbody>().isKinematic = true;
                _CamRayTarget.transform.parent = m_RightArmTransform;
                _CamRayTarget.transform.position = m_RightArmTransform.position;
                _CamRayTarget.transform.rotation = m_RightArmTransform.rotation;
                _gameUIManager.m_TabImg_SecondaryWeap.sprite = _CamRayTarget.GetComponent<Weapon>().m_ImgWeaponIcon;
                _gameUIManager.m_SecondaryAmmoIcon.sprite = _CamRayTarget.GetComponent<Weapon>().m_ImgAmmoIcon;

                // 임시방편으로 해둔 거라 수정해야 함.
                if (_CamRayTarget.GetComponent<Weapon>().m_strName == "M1911") 
                {
                    _gameUIManager.m_Ammo_SubWeap.gameObject.SetActive(true);
                    _gameUIManager.m_InfinityIcon.gameObject.SetActive(false);
                    //_gameUIManager.m_SecondaryAmmoIcon.gameObject.SetActive(true);
                }
                else
                {
                    _gameUIManager.m_Ammo_SubWeap.gameObject.SetActive(false);
                    _gameUIManager.m_InfinityIcon.gameObject.SetActive(true);
                    //_gameUIManager.m_SecondaryAmmoIcon.gameObject.SetActive(false);
                }
                //////////////////////////////////////

                if (m_eSelectedItem == Item.Primary)
                    _CamRayTarget.SetVisible(false, true);
                else
                    _CamRayTarget.SetVisible(true, true);
                break;

            case Item.Throwing:
                break;
            case Item.Healing:
                break;
            default:
                break;
        }
        m_Item[(int)_CamRayTarget.GetComponent<Weapon>().Get_ItemType()] = _CamRayTarget;
    }
    private void SpawnThrowItem()
    {
        m_Item[(int)Item.Throwing] = Instantiate(m_PrefabThrowWeapon, m_RightArmTransform);
        m_Item[(int)Item.Throwing].transform.localScale = new Vector3(50f, 50f, 50f);
        IThrowWeaponScript throwWeaponScript = m_Item[(int)Item.Throwing].GetComponent<IThrowWeaponScript>();
        m_ThrowWeapon = throwWeaponScript.GetThrowWeapon();
    }

    private void Using_OBJ()
    {
        if (_fDis_PlayerAndOBJ < _fRangeToPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_CamRayTarget.layer == LayerMask.NameToLayer("StaticOBJ"))
                {
                    //_HP += 50;
                }
                else if (_CamRayTarget.layer == LayerMask.NameToLayer("ShopOBJ"))
                {
                    _mainMenuUI.OpenToShop();
                    m_Animator.SetBool("Walk", false);
                }
                else if (_CamRayTarget.layer == LayerMask.NameToLayer("TakeWeapOBJ"))
                {
                    
                    ChangeTo_GetDropItem();
                }
                else if(_CamRayTarget.layer == LayerMask.NameToLayer("StaticInteractOBJ"))
                {
                    IInteractE interactE;
                    if (_CamRayTarget.TryGetComponent(out interactE))
                    {
                        interactE.InteractE();
                    }
                    else
                    {
                        Debug.LogWarning(ToString() + ", IInteractE 못찾음");
                    }
                }
                else if (_CamRayTarget.layer == LayerMask.NameToLayer("RideOBJ"))
                {
                    m_HeavyGun = _CamRayTarget.GetComponent<HeavyGun>();

                    m_HeavyGun.Player_RideTo_HeavyGun();
                }
                else if (_CamRayTarget.layer == LayerMask.NameToLayer("CanTakeOBJ"))
                {
                    m_COIN_Script = _CamRayTarget.GetComponent<CoinScript>();
                    m_SoundScript.Play_JustOne(0);
                    m_iPocketMoney += m_COIN_Script.Take_Coin();
                    m_MainUI_Head.Add_PlayerMoneyEffect();
                }
            }
        }
    }

    private void PopUpToggle() 
    {
        if (_fDis_PlayerAndOBJ < _fRangeToPlayer &&
            _CamRayTarget.layer != LayerMask.NameToLayer("UI") &&
            _CamRayTarget.layer != LayerMask.NameToLayer("Player") &&
            _CamRayTarget.layer != LayerMask.NameToLayer("Terrain"))
        {
            if (_CamRayTarget.layer == LayerMask.NameToLayer("TakeWeapOBJ"))
            {
                _gameUIManager.m_InteractionUI_TEXT.text = _CamRayTarget.GetComponent<Weapon>().m_strName + "(으)로 교체하기";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }

            if (_CamRayTarget.layer == LayerMask.NameToLayer("StaticOBJ") &&
                _CamRayTarget.layer != LayerMask.NameToLayer("TakeWeapOBJ"))
            {
                _gameUIManager.m_InteractionUI_TEXT.text = "상호작용";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }

            if (_CamRayTarget.layer == LayerMask.NameToLayer("ShopOBJ") &&
                _CamRayTarget.layer != LayerMask.NameToLayer("TakeWeapOBJ"))
            {
                _gameUIManager.m_InteractionUI_TEXT.text = "상점열기";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }

            if (_CamRayTarget.layer == LayerMask.NameToLayer("StaticInteractOBJ") &&
                _CamRayTarget.layer != LayerMask.NameToLayer("TakeWeapOBJ"))
            {
                _gameUIManager.m_InteractionUI_TEXT.text = "사용하기";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }

            if (_CamRayTarget.layer == LayerMask.NameToLayer("CanTakeOBJ") &&
                _CamRayTarget.layer != LayerMask.NameToLayer("TakeWeapOBJ"))
            {
                m_COIN_Script = _CamRayTarget.GetComponent<CoinScript>();
                _gameUIManager.m_InteractionUI_TEXT.text = "줍기";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }

            if (_CamRayTarget.layer == LayerMask.NameToLayer("RideOBJ"))
            {
                m_HeavyGun = _CamRayTarget.GetComponent<HeavyGun>();
                _gameUIManager.m_InteractionUI_TEXT.text = "장비 탑승";
                _gameUIManager.m_InteractionUI.SetActive(true);
            }
        }
        else
            _gameUIManager.m_InteractionUI.SetActive(false);
    }

    public void SetAnimatorParameterBool(string str, bool _bool)
    {
        m_Animator.SetBool(str, _bool);
    }

    public void SetAnimatorParameterTrigger(string str)
    {
        m_Animator.SetTrigger(str);
    }

    public void SetPrefabThrowWeapon(GameObject _Prefab)
    {
        m_PrefabThrowWeapon = _Prefab;
    }

    public void MeleeAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(m_SecondaryWeapon.transform.GetTransformInAllChildren("AtkPos").position, 3f,
            (1 << LayerMask.NameToLayer("Enemy")));
        foreach (Collider collider in colliders)
        {
            //collider.gameObject.TryGetComponent
            IDamageReceiver receiver;
            collider.TryGetComponent(out receiver);
            if (null != receiver)
            {
                receiver.GiveDamage(m_SecondaryWeapon.m_WeaponInfo.fAtk);
            }

            Instantiate(m_BloodSprayEffect, collider.ClosestPoint(m_SecondaryWeapon.transform.GetTransformInAllChildren("AtkPos").position), collider.transform.rotation);
        }
    }

    public void MinusPocketMoney(int _Price)
    {
        m_SoundScript.Play_JustOne(0);
        m_iPocketMoney -= _Price;
    }

    //private void OnDrawGizmos()
    //{
    //    if(m_SecondaryWeapon.m_WeaponInfo.eType == Weapon.Type.Melee)
    //        Gizmos.DrawWireSphere(m_SecondaryWeapon.transform.GetTransformInAllChildren("AtkPos").position, 3f);
    //}
}
