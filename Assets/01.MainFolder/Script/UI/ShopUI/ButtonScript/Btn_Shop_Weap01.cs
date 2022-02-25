using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Shop_Weap01 : MonoBehaviour
{
    Shop_Menu _ShopMenu;
    PlayerStatus m_PlayerStat;
    NowEquipmentBox m_NowEquipmentBox;
    InGameUIMgr m_InGameUIMgr;

    GameObject m_BtnWeap01;
    GameObject m_BtnWeap02;
    GameObject m_PriceBG;

    int m_iWeaponNUM;

    Image m_Image_Btn_Weap01;
    Image m_Image_CoinIcon;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _ShopMenu = GameObject.Find("Shop UI Panel 1").GetComponent<Shop_Menu>();
        m_NowEquipmentBox = GameObject.Find("Equip_Panel").GetComponent<NowEquipmentBox>();
        m_Image_Btn_Weap01 = GameObject.Find("Image Weap01").GetComponent<Image>();
        m_InGameUIMgr = GameObject.Find("GameUI Manager").GetComponent<InGameUIMgr>();
        m_PriceBG = GameObject.Find("Weap01 PriceBG");
        m_Image_CoinIcon = GameObject.Find("Weap01 Coin Icon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Image_Btn_Weap01.sprite = WeaponList.m_tWeapInfo[m_iWeaponNUM]._WeapImage;
        _ShopMenu.m_txtPrice01.text = string.Format("{0:n0}", WeaponList.m_tWeapInfo[m_iWeaponNUM]._iPrice);

        ChangeColorToMoney();
    }
    public void MouseOver()
    {
        if (!_ShopMenu._bOn_ItemInfo)
        {
            _ShopMenu.m_txtName.text = WeaponList.m_tWeapInfo[m_iWeaponNUM]._strName;
            _ShopMenu.m_txtKind.text = WeaponList.m_tWeapInfo[m_iWeaponNUM]._strKind;
            _ShopMenu.m_txtCharTitle.text = WeaponList.m_tWeapInfo[m_iWeaponNUM]._strCharTitle;
            _ShopMenu.m_txtChar.text = WeaponList.m_tWeapInfo[m_iWeaponNUM]._strChar;
            _ShopMenu.m_txtExplane.text = WeaponList.m_tWeapInfo[m_iWeaponNUM]._strExplane;
            _ShopMenu.m_Info_WeapImage.sprite = WeaponList.m_tWeapInfo[m_iWeaponNUM]._WeapImage;

            _ShopMenu._bOn_ItemInfo = true;
            _ShopMenu._bOn_WeapInfo = true;
        }
        else
        {
            _ShopMenu._bOn_ItemInfo = false;
            _ShopMenu._bOn_WeapInfo = false;
        }
    }
    public void MouseClick()
    {
        if (m_Image_Btn_Weap01.sprite != m_NowEquipmentBox.m_PrimaryImage.sprite &&
            m_Image_Btn_Weap01.sprite != m_NowEquipmentBox.m_SecondaryImage.sprite)
        {
            if (m_PlayerStat.m_iPocketMoney >= WeaponList.m_tWeapInfo[m_iWeaponNUM]._iPrice)
            {
                m_PlayerStat.MinusPocketMoney(WeaponList.m_tWeapInfo[m_iWeaponNUM]._iPrice);

                if (WeaponList.m_tWeapInfo[m_iWeaponNUM]._tWeapType == WeaponList.WeapType.Secondary)
                {
                    m_PlayerStat.m_iSecondaryWeaponNUM = m_iWeaponNUM;
                    m_NowEquipmentBox.m_SecondaryImage.sprite = m_Image_Btn_Weap01.sprite;
                    m_InGameUIMgr.m_TabImg_SecondaryWeap.sprite = m_Image_Btn_Weap01.sprite;
                    m_InGameUIMgr.m_SecondaryAmmoIcon.sprite = WeaponList.m_tWeapInfo[m_iWeaponNUM]._AmmoImage;

                    if (m_iWeaponNUM == (int)WeaponList.WeaponName.Hammer ||
                        m_iWeaponNUM == (int)WeaponList.WeaponName.Baseball_Bat ||
                        m_iWeaponNUM == (int)WeaponList.WeaponName.Katana)
                    {
                        m_NowEquipmentBox.m_txtSecondaryWeapMaxAmmo.gameObject.SetActive(false);
                        m_NowEquipmentBox.m_Icon_Infinity.gameObject.SetActive(true);
                        m_InGameUIMgr.m_InfinityIcon.gameObject.SetActive(true);
                        m_InGameUIMgr.m_Ammo_SubWeap.gameObject.SetActive(false);
                    }

                    else
                    {
                        m_NowEquipmentBox.m_txtSecondaryWeapMaxAmmo.gameObject.SetActive(true);
                        m_NowEquipmentBox.m_Icon_Infinity.gameObject.SetActive(false);
                        m_InGameUIMgr.m_InfinityIcon.gameObject.SetActive(false);
                        m_InGameUIMgr.m_Ammo_SubWeap.gameObject.SetActive(true);
                    }

                    m_PlayerStat.m_bChangeSecondaryWeap = true;
                }

                if (WeaponList.m_tWeapInfo[m_iWeaponNUM]._tWeapType == WeaponList.WeapType.Primary)
                {
                    m_PlayerStat.m_iPrimaryWeaponNUM = m_iWeaponNUM;
                    m_NowEquipmentBox.m_PrimaryImage.sprite = m_Image_Btn_Weap01.sprite;
                    m_InGameUIMgr.m_TabImg_PrimaryWeap.sprite = m_Image_Btn_Weap01.sprite;
                    m_InGameUIMgr.m_PrimaryAmmoIcon.sprite = WeaponList.m_tWeapInfo[m_iWeaponNUM]._AmmoImage;
                    m_PlayerStat.m_bChangePrimaryWeap = true;
                }
            }
        }
    }
    public void RandomSetWeapon()
    {
        m_iWeaponNUM = Random.Range(0, (int)WeaponList.WeaponName.End);
    }
    private void ChangeColorToMoney()
    {
        if (WeaponList.m_tWeapInfo[m_iWeaponNUM]._iPrice > m_PlayerStat.m_iPocketMoney)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
            m_PriceBG.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            m_Image_CoinIcon.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            m_PriceBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.8f);
            m_Image_CoinIcon.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
