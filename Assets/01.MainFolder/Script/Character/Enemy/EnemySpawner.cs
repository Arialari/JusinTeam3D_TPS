using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject m_zombie;

    public AIViewPerception.Alert m_eAlert { get; set; } = AIViewPerception.Alert.Safe;

    private const float m_fSpawnCoolTime = 20f;
    private float m_fSpawnRemainTime = 0f;
    //public GameObject m_rooter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ExtractionQuestManager.GetInstance().GetMission() > ExtractionQuestManager.Mission.Get_To_Middle)
        {
            Destroy(gameObject);
            return;
        }
        UpdateSpawn();
    }
    private void UpdateSpawn()
    {
        if(m_eAlert == AIViewPerception.Alert.Alert)
        {
            m_fSpawnRemainTime -= Time.deltaTime;
            if(m_fSpawnRemainTime <= 0f)
            {
                m_fSpawnRemainTime = m_fSpawnCoolTime;
                Spawn();
            }
        }       
    }
    private void Spawn()
    {
        GameObject enemy = GameObject.Instantiate(m_zombie, transform.position, transform.rotation);
    }
}
