using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTargetPointer : MonoBehaviour
{
    public GameObject m_ImagePrefab;
    private Camera _MainCam;
    private Transform _PlayerTransform;
    private GameObject m_TargetPointerUI;
    private Image m_Image;
    private Text m_Text;

    private float m_fDistance = 0f;
    private bool m_bIsNotCulling = true;
    private bool m_bIsVisible = false;
    private float m_fOpacity = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        _MainCam = Camera.main;
        _PlayerTransform = GameObject.Find("Player").transform;
        m_TargetPointerUI = Instantiate(m_ImagePrefab);
        m_TargetPointerUI.transform.GetChild(0).TryGetComponent(out m_Image);
        m_Image.transform.GetChild(0).TryGetComponent(out m_Text);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bIsVisible)
            return;
        UpdateImage();
        UpdateText();
    }
    private void UpdateImage()
    {
        Vector3 pointerPos = _MainCam.WorldToScreenPoint(transform.position);
        m_fDistance = Vector3.Distance(transform.position, _PlayerTransform.position) * 0.25f;
        m_fOpacity = 1 - Mathf.Clamp(m_fDistance, 0, 50) * 0.01f;

        m_bIsNotCulling = (0 < pointerPos.z);
        m_Image.enabled = m_bIsNotCulling;
        m_Image.transform.position = pointerPos;
        m_Image.color = new Color(1f, 1f, 1f, m_fOpacity);
    }
    private void UpdateText()
    {
        m_Text.enabled = m_bIsNotCulling;
        m_Text.text = Mathf.RoundToInt(m_fDistance).ToString() + "m";
        m_Text.color = new Color(1f, 1f, 1f, m_fOpacity);
    }

    public void SetVisible(bool _bool)
    {
        m_bIsVisible = _bool;
        if(!_bool)
        {
            m_Image.enabled = false;
            m_Text.enabled = false;
        }
    }
}
