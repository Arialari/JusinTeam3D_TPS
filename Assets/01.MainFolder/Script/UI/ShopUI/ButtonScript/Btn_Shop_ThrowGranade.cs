using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Shop_ThrowGranade : MonoBehaviour
{
    Shop_Menu _ShopMenu;
    PlayerStatus m_PlayerStat;
    NowEquipmentBox m_NowEquipBox;
    InGameUIMgr m_InGameUIMgr;

    Image _Granade_Image;
    Image m_Icon_Coin;
    Image m_Img_PriceBG;


    // Start is called before the first frame update
    void Start()
    {
        _ShopMenu = GameObject.Find("Shop UI Panel 1").GetComponent<Shop_Menu>();
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _Granade_Image = GameObject.Find("Image Granade").GetComponent<Image>();
        m_Img_PriceBG = GameObject.Find("Grenade PriceBG").GetComponent<Image>();
        m_Icon_Coin = GameObject.Find("Grenade Coin Icon").GetComponent<Image>();
        m_NowEquipBox = GameObject.Find("Equip_Panel").GetComponentInParent<NowEquipmentBox>();
        m_InGameUIMgr = GameObject.Find("GameUI Manager").GetComponent<InGameUIMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColorToMoney();
    }

    public void MouseOver()
    {
        if (!_ShopMenu._bOn_ItemInfo)
        {
            _ShopMenu.m_Info_ItemImage.sprite = _Granade_Image.sprite;

            _ShopMenu.m_txtName.text = WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._strName;
            _ShopMenu.m_txtKind.text = WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._strKind;
            _ShopMenu.m_txtCharTitle.text = WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._strCharTitle;
            _ShopMenu.m_txtChar.text = WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._strChar;
            _ShopMenu.m_txtExplane.text = WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._strExplane;

            _ShopMenu._bOn_ItemInfo = true;
        }
        else
            _ShopMenu._bOn_ItemInfo = false;
    }
    public void MouseClick()
    {
        if (m_PlayerStat.m_iPocketMoney >= WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._iPrice)
        {
            if (m_NowEquipBox.m_Icon_Throwable.sprite != _Granade_Image.sprite)
            {
                m_PlayerStat.MinusPocketMoney(WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._iPrice);
                m_PlayerStat.m_iThrowableNUM = (int)WeaponList.ItemName.Grenade;
                m_NowEquipBox.m_Icon_Throwable.sprite = _Granade_Image.sprite;
                m_InGameUIMgr.m_TabImg_Throwalble.sprite = _Granade_Image.sprite;
                m_PlayerStat.m_iAmmo_Throwable = 1;
                m_PlayerStat.m_bChangeThrowable = true;
            }

            else if (m_PlayerStat.m_iAmmo_Throwable < 9)
            {
                m_PlayerStat.m_iPocketMoney -= WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._iPrice;
                m_PlayerStat.m_iAmmo_Throwable += 1;
            }

            
        }
    }
    private void ChangeColorToMoney()
    {
        if (m_PlayerStat.m_iPocketMoney < WeaponList.m_tItemInfo[(int)WeaponList.ItemName.Grenade]._iPrice)
        {
            this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            m_Img_PriceBG.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            m_Icon_Coin.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }

        else
        {
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            m_Img_PriceBG.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            m_Icon_Coin.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
