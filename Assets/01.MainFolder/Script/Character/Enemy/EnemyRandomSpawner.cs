using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyRandomSpawner : MonoBehaviour
{
    public GameObject m_ZombiePrefab;
    public GameObject m_BreacherPrefab;
    public GameObject m_RooterPrefab;

    public float m_fSpawnCoolTime = 10.0f;
    [Range(0,1)]public int m_iRandomMin = 0;

    [SerializeField]
    public List<Vector3> m_vSpawnLocations;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        for(int i=0; i<m_vSpawnLocations.Count; ++i)
        {
            int iRandom = Random.Range(m_iRandomMin, 10);
            GameObject spawnObject;
            if (0 == iRandom)
            {
                spawnObject = m_RooterPrefab;
            }
            else if (7 >= iRandom)
            {
                spawnObject = m_ZombiePrefab;
            }
            else
            {
                spawnObject = m_BreacherPrefab;
            }

            GameObject spawned = Instantiate(spawnObject, m_vSpawnLocations[i], transform.rotation);
            AIViewPerception aIViewPerception;
            spawned.TryGetComponent(out aIViewPerception);
            aIViewPerception.m_eAlert = AIViewPerception.Alert.Alert;
        }
        

        Invoke("Spawn", m_fSpawnCoolTime);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i=0; i<m_vSpawnLocations.Count; ++i)
        {
            Gizmos.DrawSphere(m_vSpawnLocations[i],1f);
        }
    }
#endif
}
