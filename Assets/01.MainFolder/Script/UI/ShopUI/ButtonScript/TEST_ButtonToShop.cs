using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST_ButtonToShop : MonoBehaviour
{
    Shop_Menu _ShopMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ShopMenu = GameObject.Find("Shop UI Panel 1").GetComponent<Shop_Menu>();

    }

    public void MouseOver()
    {
        if (!_ShopMenu._bOn_ItemInfo)
        {
            _ShopMenu.m_Info_ItemImage.sprite = Resources.Load("AMMO/AMMO_HG", typeof(Sprite)) as Sprite;
            _ShopMenu._bOn_ItemInfo = true;
        }
        else
            _ShopMenu._bOn_ItemInfo = false;
    }
}
