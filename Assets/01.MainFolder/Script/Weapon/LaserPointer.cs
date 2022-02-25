using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    public Transform m_MuzzleTransform;
    private PlayerStatus m_PlayerStatus;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LineRenderer.enabled)
            return;

        else
        {
            m_LineRenderer.SetPosition(0, m_MuzzleTransform.position);
            RaycastHit hit;

            if (m_PlayerStatus.GetPrimaryWeapon().m_WeaponInfo.eType != Weapon.Type.SR)
            {
                if (Physics.Raycast(m_MuzzleTransform.position, Camera.main.transform.forward, out hit))
                {
                    if (hit.collider)
                    {
                        m_LineRenderer.SetPosition(1, hit.point);
                    }
                }

                else
                {
                    m_LineRenderer.SetPosition(1, Camera.main.transform.forward * 5000);
                }
            }
        }
    }

    public void Laser_OnOff(bool _OnOff)
    {
        m_LineRenderer.enabled = _OnOff;
        //this.gameObject.SetActive(_OnOff);
    }

    public void SetMuzzleTransform(Transform _Muzzletrans)
    {
        m_MuzzleTransform = _Muzzletrans;
    }
}
