using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    public bool _bOpenUI;

    public Transform m_ObjectToFollow;
    private float fFollowSpeed;
    public float fMouseSensitive;
    public float fClampAngle = 89.9f;
    public Camera _TPScamera;
    public Camera m_SniperCam;

    private float fRotX;
    private float fRotY;

    public Transform MainCam;
    public Vector3 vDirNormalized;
    public Vector3 vFinalDir;
    public float fMinCamDis;
    public float fMaxCamDis;
    public float fFinalCamDis;
    public float fSmoothness;

    PlayerStatus m_PlayerStat;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();

        fRotX = transform.localRotation.eulerAngles.x;
        fRotY = transform.localRotation.eulerAngles.y;

        vDirNormalized = MainCam.localPosition.normalized;
        fFinalCamDis = MainCam.localPosition.magnitude;

        fFollowSpeed = 1000.0f;
        _bOpenUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_bOpenUI)
        {
            if (m_PlayerStat.GetAnimator().GetBool("HeavyGrip"))
            {
                _TPScamera.enabled = false;
            }

            if (!m_PlayerStat.GetAnimator().GetBool("HeavyGrip"))
            {
                LookAround();
                Sniper_Aiming();
            }
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, m_ObjectToFollow.position, fFollowSpeed * Time.deltaTime);

        vFinalDir = transform.TransformPoint(vDirNormalized * fMaxCamDis);

        RaycastHit Hit;

        if (Physics.Linecast(transform.position, vFinalDir, out Hit, LayerMask.GetMask("Background","StaticInteractOBJ","BreakableWall")))
        {
            fFinalCamDis = Mathf.Clamp(Hit.distance, fMinCamDis, fMaxCamDis);
        }

        else
            fFinalCamDis = fMaxCamDis;

        MainCam.localPosition = Vector3.Lerp(MainCam.localPosition, vDirNormalized * fFinalCamDis, Time.deltaTime * fSmoothness);
    }

    public void CamOn()
    {
        _TPScamera.enabled = true;
    }

    void LookAround()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        fRotX -= Input.GetAxis("Mouse Y") * fMouseSensitive * Time.deltaTime;
        fRotY += Input.GetAxis("Mouse X") * fMouseSensitive * Time.deltaTime;

        // X축 각도 제한
        fRotX = Mathf.Clamp(fRotX, -fClampAngle, fClampAngle);

        Quaternion Rot = Quaternion.Euler(fRotX, fRotY, 0f);
        transform.rotation = Rot;
    }

    public void Sniper_Aiming()
    {
        if (m_PlayerStat.GetPrimaryWeapon().m_WeaponInfo.eType == Weapon.Type.SR &&
        m_PlayerStat.GetSelectedItem() == PlayerStatus.Item.Primary)
        {
            if(m_SniperCam == null)
                m_SniperCam = m_PlayerStat.GetPrimaryWeapon().transform.GetTransformInAllChildren("SniperZoomCam").GetComponent<Camera>();
            
            if (Input.GetMouseButton(1))
            {
                m_SniperCam.enabled = true;
                _TPScamera.enabled = false;

                m_SniperCam.fieldOfView = 10;
                m_SniperCam.transform.GetTransformInAllChildren("Canvas").gameObject.SetActive(true);
            }

            else if (Input.GetMouseButtonUp(1))
            {
                _TPScamera.enabled = true;
                m_SniperCam.enabled = false;
                m_SniperCam.transform.GetTransformInAllChildren("Canvas").gameObject.SetActive(false);
            }

            else
            {
                _TPScamera.enabled = true;
                m_SniperCam.enabled = false;
            }

        }

        else
        {
            if (Input.GetMouseButton(1))
            {
                _TPScamera.fieldOfView = 30;
            }
            else if (Input.GetMouseButtonUp(1))
                _TPScamera.fieldOfView = 50;

            else
                _TPScamera.fieldOfView = 50;
        }
    }
}

