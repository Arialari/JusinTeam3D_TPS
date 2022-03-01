using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptUI : MonoBehaviour
{
    public Text m_Text;
    public RawImage m_Image;
    private CanvasGroup m_CanvasGroup;

    void Awake()
    {
        TryGetComponent(out m_CanvasGroup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _script)
    {
        m_Text.text = _script;
    }

    public void SetImage(Texture texture)
    {
        m_Image.texture = texture;
    }

    public void SetVisibility(bool _IsVisible)
    {
        m_CanvasGroup.alpha = _IsVisible ? 1 : 0;
    }
}
