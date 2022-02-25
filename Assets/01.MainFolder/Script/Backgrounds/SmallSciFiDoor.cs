using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSciFiDoor : MonoBehaviour
{
    Animation m_animation;
    private bool m_bIsOpen = false;
    void Awake()
    {
        TryGetComponent(out m_animation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleDoor()
    {
        if(m_bIsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
        m_bIsOpen = !m_bIsOpen;
    }

    private void Open()
    {
        m_animation.Play("SmallDoorOpen");
    }
    private void Close()
    {
        m_animation.Play("SmallDoorClose");
    }
}
