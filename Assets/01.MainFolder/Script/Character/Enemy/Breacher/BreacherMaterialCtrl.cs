using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreacherMaterialCtrl : MonoBehaviour
{
    public List<GameObject> m_Crystals;
    private Material[] m_matCrystals;
    private BreacherAI m_BreacherAI;
    private Color orange = new Color(1.0f, 0.7f, 0.1f,1.0f);
    public bool m_bIsExploding { set; private get; } = false;
    private const float m_fEXPLODE_TIME = 2f;
    private float m_fExplodeAccuTime = 0f;
    void Awake()
    {
        m_matCrystals = new Material[m_Crystals.Count];
        for (int i=0; i < m_Crystals.Count; ++i)
        {
            MeshRenderer meshRenderer;
            if(m_Crystals[i].TryGetComponent(out meshRenderer))
            {
                m_matCrystals[i] = meshRenderer.material;
                m_matCrystals[i].EnableKeyword("_EMISSION");
            }
            else
            {
                Debug.LogError(ToString() + ", meshRenderer[" + i.ToString() + "]를 찾지 못했습니다");
            }
        }
        if(!TryGetComponent(out m_BreacherAI))
        {
            Debug.LogError(ToString() + ", BreacherAI 못찾음");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExplode();
    }

    private void UpdateExplode()
    {
        if (!m_bIsExploding)
        {
            return;
        }
        m_fExplodeAccuTime += Time.deltaTime;
        float fExplodeAlpha = m_fExplodeAccuTime / m_fEXPLODE_TIME;
        for (int i = 0; i < m_matCrystals.Length; ++i)
        {
            m_matCrystals[i].SetColor("_EmissionColor", orange * fExplodeAlpha);
        }
        if(1f <= fExplodeAlpha)
        {
            m_BreacherAI.Explode();
        }
    }
}
