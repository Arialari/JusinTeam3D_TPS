using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab_Shop_InHandAmmoPanel : MonoBehaviour
{
    PlayerStatus m_PlayerStat;

    Text m_txtHG;
    Text m_txtAR;
    Text m_txtSR;
    Text m_txtSG;

    // Start is called before the first frame update
    void Start()
    {
        m_txtHG = GameObject.Find("Text_Ammo_HG").GetComponent<Text>();
        m_txtAR = GameObject.Find("Text_Ammo_AR").GetComponent<Text>();
        m_txtSR = GameObject.Find("Text_Ammo_SR").GetComponent<Text>();
        m_txtSG = GameObject.Find("Text_Ammo_SG").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();

        m_txtHG.text = string.Format("{0:D3}", m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.HG]);
        m_txtAR.text = string.Format("{0:D3}", m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.AR]);
        m_txtSR.text = string.Format("{0:D3}", m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.SR]);
        m_txtSG.text = string.Format("{0:D3}", m_PlayerStat.m_iTotalAmmo[(int)Weapon.Type.SG]);
    }
}
