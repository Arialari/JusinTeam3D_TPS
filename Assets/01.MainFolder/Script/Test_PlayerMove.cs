using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerMove : MonoBehaviour
{
    [HideInInspector] public Transform m_Target;
    [HideInInspector] public Vector3 m_vRelaticeVec;

    private Animator m_Animator;
    private Transform m_SpineTrans;
    private Camera m_Camera;
    bool m_bHoldToView;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_SpineTrans = m_Animator.GetBoneTransform(HumanBodyBones.Spine);
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!m_Animator.GetBool("Reload"))
        {
            if (Input.GetMouseButton(1)) // 우클릭 눌리는 동안
            {
                StartCoroutine(aimModeOn());
            }

            else if (Input.GetMouseButtonUp(1)) // 우클릭을 떄면
            {
                StartCoroutine(aimModeOff());
            }

            if (m_bHoldToView == true) //에임모드가 활성화 되면
            {
                m_SpineTrans.LookAt(m_Camera.transform.position + m_Camera.transform.forward * GunCtrl.m_fRayDistance); //플레이어의 상체부분이 타겟 위치 보기
                m_SpineTrans.rotation = m_SpineTrans.rotation * Quaternion.Euler(m_vRelaticeVec); // 타겟으로 회전
                m_SpineTrans.localRotation *= Quaternion.Euler(0, -45.0f, -90.0f); // 상체 틀어짐 방지
            }
        }
    }

    IEnumerator aimModeOn()
    {
        yield return new WaitForSeconds(0.05f);
        m_Animator.SetBool("Battle", true);
        m_Animator.SetBool("Idle", false);
        m_Animator.SetBool("Aiming", true);
        m_bHoldToView = true;
    }

    IEnumerator aimModeOff()
    {
        yield return new WaitForSeconds(0.1f);
        //m_Animator.SetBool("Battle", false);
        m_Animator.SetBool("Aiming", false);
        m_Animator.SetBool("Idle", true);
        m_bHoldToView = false;
    }
}
