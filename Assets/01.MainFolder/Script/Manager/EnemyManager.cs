using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager m_Instance;
    private List<AIViewPerception> m_EnemyViewPerceptions = new List<AIViewPerception>();
    public delegate bool GoingToDestroy(GameObject enemy);

    void Awake()
    {
        m_Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static EnemyManager GetInstance()
    {
        return m_Instance;
    }

    public void AddAIViewPerception(AIViewPerception _aIViewPerception)
    {
        m_EnemyViewPerceptions.Add(_aIViewPerception);
    }

    public void DestroyEnemys(GoingToDestroy goingToDestroy)
    {
        for(int i=m_EnemyViewPerceptions.Count-1; i >= 0; --i)
        {
            if (!m_EnemyViewPerceptions[i])
                continue;
            if(goingToDestroy(m_EnemyViewPerceptions[i].gameObject))
            {
                Destroy(m_EnemyViewPerceptions[i].gameObject);
            }
        }
    }
    public bool IsEmpty()
    {
        for(int i=0; i < m_EnemyViewPerceptions.Count; ++i)
        {
            if (m_EnemyViewPerceptions[i])
                return false;
        }
        return true;
    }
}
