using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Shop_AmmoSG : MonoBehaviour
{
    Shop_Menu _ShopMenu;
    Image _AmmoSG_Image;
    PlayerStatus m_PlayerStat;
    Image m_Icon_Coin;
    Image m_Img_PriceBG;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _ShopMenu = GameObject.Find("Shop UI Panel 1").GetComponent<Shop_Menu>();
        _AmmoSG_Image = GameObject.Find("Icon_AmmoSG").GetComponent<Image>();
        m_Img_PriceBG = GameObject.Find("SG PriceBG").GetComponent<Image>();
        m_Icon_Coin = GameObject.Find("SG Coin Icon").GetComponent<Image>();
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
            _ShopMenu.m_Info_ItemImage.sprite = _AmmoSG_Image.sprite;

            _ShopMenu.m_txtName.text = "»êÅºÃÑ Åº¾à";
            _ShopMenu.m_txtKind.text = "Åº¾à";
            _ShopMenu.m_txtCharTitle.text = "Åº¼ö·®:";
            _ShopMenu.m_txtChar.text = "10¹ß";
            _ShopMenu.m_txtExplane.text = "»êÅºÃÑ¿¡ »ç¿ëµÇ´Â Åº¾àÀÌ´Ù.";

            _ShopMenu._bOn_ItemInfo = true;
        }
        else
            _ShopMenu._bOn_ItemInfo = false;
    }
    public void MouseClick()
    {
        if (m_PlayerStat.m_iPocketMoney >= 50 &&
            m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.SG] < 999)
        {
            m_PlayerStat.MinusPocketMoney(50);
            m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.SG] += 10;
        }
    }
    private void ChangeColorToMoney()
    {
        if (m_PlayerStat.m_iPocketMoney < 50)
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
