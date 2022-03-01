using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Using_GageBar : MonoBehaviour
{
    public Text m_UsingText;
    public Image m_Using_Gage;

    private float m_fUsingTime;
    private float m_fUsingAccTime = 0f;

    private string m_strUsingObjName = "";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        m_UsingText.text = m_strUsingObjName + " »ç¿ë Áß..";
    }

    public void Filling_Gage()
    {
        gameObject.SetActive(true);
        m_fUsingAccTime += Time.deltaTime;
        m_Using_Gage.fillAmount = m_fUsingAccTime / m_fUsingTime;
    }

    public void Stop_Filling()
    {
        gameObject.SetActive(false);
        m_fUsingAccTime = 0;
        m_Using_Gage.fillAmount = 0;
    }

    public void Set_UsingObjName(string _strObjName)
    {
        m_strUsingObjName = _strObjName;
    }

    public void Set_UsingTime(float _UsingTime)
    {
        m_fUsingTime = _UsingTime;
    }
}
