using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketMoneyBox : MonoBehaviour
{
    PlayerStatus m_PlayerStat;
    Text m_txtPocketMoney;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        m_txtPocketMoney = GameObject.Find("PocketMoney").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       m_txtPocketMoney.text = string.Format("{0:n0}", m_PlayerStat.m_iPocketMoney);
    }

}
