using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTargetManager : MonoBehaviour
{
    public List<QuestTargetPointer> m_QuestObjectList;
    public List<int> m_QuestMount;
    static private QuestTargetManager m_Instance;

    private int m_QuestIndex = 0;
    public static QuestTargetManager GetInstance()
    {
        return m_Instance;
    }
    
    void Awake()
    {
        m_Instance = this;
    }
    void Start()
    {
        m_QuestObjectList[0].SetVisible(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CompleteCurrentQuest()
    {
        if(m_QuestIndex >= 0)
        {
            if(m_QuestObjectList[m_QuestIndex])
                m_QuestObjectList[m_QuestIndex].SetVisible(false);
        }
    }

    public void StartNextQuest()
    {
        if (m_QuestObjectList.Count <= ++m_QuestIndex)
        {
            Debug.LogWarning(ToString() + ", No Next Quest");
            return;
        }
        if(m_QuestObjectList[m_QuestIndex])
            m_QuestObjectList[m_QuestIndex].SetVisible(true);
    }
}
