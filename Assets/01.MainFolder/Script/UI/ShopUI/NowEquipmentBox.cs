using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowEquipmentBox : MonoBehaviour
{
    PlayerStatus m_PlayerStat;
    InGameUIMgr m_InGameUIMgr;
    Text m_txtPrimaryWeapMaxAmmo;
    [HideInInspector] public Text m_txtSecondaryWeapMaxAmmo;
    [HideInInspector] public Text m_txtThrowableMaxAmmo;
    [HideInInspector] public Text m_txtMedicineMaxAmmo;

    [HideInInspector] public Image m_PrimaryImage;
    [HideInInspector] public Image m_SecondaryImage;
    [HideInInspector] public Image m_PrimaryAmmoImg;
    [HideInInspector] public Image m_SecondaryAmmoImg;
    [HideInInspector] public Image m_Icon_Infinity;
    [HideInInspector] public Image m_Icon_Throwable;
    [HideInInspector] public Image m_Icon_Medicine;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        m_InGameUIMgr = GameObject.Find("GameUI Manager").GetComponent<InGameUIMgr>();
        m_txtPrimaryWeapMaxAmmo = GameObject.Find("Ammo_MW-Max").GetComponent<Text>();
        m_txtSecondaryWeapMaxAmmo = GameObject.Find("Ammo_SW-Max").GetComponent<Text>();
        m_txtThrowableMaxAmmo = GameObject.Find("Ammo_Throwable").GetComponent<Text>();
        m_txtMedicineMaxAmmo = GameObject.Find("Ammo_Medicine").GetComponent<Text>();
        m_PrimaryImage = GameObject.Find("Primary Image").GetComponent<Image>();
        m_SecondaryImage = GameObject.Find("Secondary Image").GetComponent<Image>();
        m_PrimaryAmmoImg = GameObject.Find("Primary_AmmoImg").GetComponent<Image>();
        m_SecondaryAmmoImg = GameObject.Find("Secondary_AmmoImg").GetComponent<Image>();
        m_Icon_Throwable = GameObject.Find("Icon_Throwable").GetComponent<Image>();
        m_Icon_Medicine = GameObject.Find("Icon_Medicine").GetComponent<Image>();
        m_Icon_Infinity = GameObject.Find("Infinity_Icon").GetComponent<Image>();

        m_Icon_Infinity.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_txtPrimaryWeapMaxAmmo.text = string.Format("{0:D3}", m_PlayerStat.GetPrimaryWeapon().GetTotalAmmo());
        m_txtSecondaryWeapMaxAmmo.text = string.Format("{0:D3}", m_PlayerStat.GetSecondaryWeapon().GetTotalAmmo());
        m_txtThrowableMaxAmmo.text = string.Format("{0:n0}", m_PlayerStat.m_iAmmo_Throwable);
        m_txtMedicineMaxAmmo.text = string.Format("{0:n0}", m_PlayerStat.m_iAmmo_Medicine);
        
        m_PrimaryImage.sprite = m_InGameUIMgr.m_TabImg_PrimaryWeap.sprite;
        m_SecondaryImage.sprite = m_InGameUIMgr.m_TabImg_SecondaryWeap.sprite;
        m_PrimaryAmmoImg.sprite = m_InGameUIMgr.m_PrimaryAmmoIcon.sprite;
        m_SecondaryAmmoImg.sprite = m_InGameUIMgr.m_SecondaryAmmoIcon.sprite;
    }
}
