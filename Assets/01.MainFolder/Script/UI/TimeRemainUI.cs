using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemainUI : MonoBehaviour
{
    public Text m_Text;
    private float m_fTimeRemain = 70.0f;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        m_fTimeRemain -= Time.deltaTime;
        if(m_fTimeRemain <= 0)
        {
            ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Hold_Until_Extract);
            Destroy(gameObject);
            return;
        }
        m_Text.text = "TimeLeft : " + Mathf.CeilToInt( m_fTimeRemain ) + "s";
    }
}
