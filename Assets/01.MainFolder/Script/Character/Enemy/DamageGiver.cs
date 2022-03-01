using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    public EnemyStatus m_EnemyStatus;
    public GameObject m_HitNavi_Canvas;
    public GameObject m_HitNaviPrefab;
    public GameObject m_Player_HPBAR;

    private MainUI_Head m_CanvasScript;

    private void Awake()
    {
        m_HitNavi_Canvas = GameObject.Find("Canvas");
        m_CanvasScript = m_HitNavi_Canvas.GetComponent<MainUI_Head>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!m_EnemyStatus.m_bIsAttacking)
            return;

        IDamageReceiver damageReceiver;
        if(!other.TryGetComponent(out damageReceiver))
        {
            return;
        }
        damageReceiver.GiveDamage(m_EnemyStatus.m_Status.fAttack);

        GameObject gameObject = Instantiate(m_HitNaviPrefab, m_HitNavi_Canvas.transform);

        gameObject.GetComponent<Hit_NaviScript>().SetEnmeyTransform(m_EnemyStatus.transform);

        m_CanvasScript.PlayerHpBox_ScaleUp();

        m_EnemyStatus.m_bIsAttacking = false;
    }
}
