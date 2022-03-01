using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Animator _Anim;
    Camera _Cam;
    CharacterController _CharCntrl;
    Rigidbody _Rigid;
    TPSCharacterController m_TPSCtrl;

    public Transform LookTransform;
    public Transform m_CharMeshTransform;

    public float fSpeed;
    public float fWalkSpeed;
    public float fRunningSpeed;
    public float fJumpPower;
    public float fSmoothness;

    float fHAxis;
    float fVAxis;
    Vector3 vMoveVec;

    private bool bRunning;
    private bool bWalking;
    private bool bJumping = false;
    private bool bToggleCamRot;

    private NavMeshAgent m_NavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        m_CharMeshTransform = GameObject.Find("Polyart_Mesh").transform;
        _Anim = m_CharMeshTransform.GetComponent<Animator>();
        _Cam = Camera.main;
        _CharCntrl = this.GetComponent<CharacterController>();
        _Rigid = this.GetComponent<Rigidbody>();
        TryGetComponent(out m_NavMeshAgent);
        m_TPSCtrl = GameObject.Find("MainCam").GetComponent<TPSCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_TPSCtrl._bOpenUI)
        {
            OnlyMoveCam();

            if(!_Anim.GetBool("HeavyGrip"))
                InputMovement();

            SetBoolAnimator();
        }
    }

        void LateUpdate()
    {
        if (!bToggleCamRot)
        {
            Vector3 vPlayerRot = Vector3.Scale(_Cam.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vPlayerRot), Time.deltaTime * fSmoothness);
        }
    }

    void SetBoolAnimator()
    {
        _Anim.SetBool("Run", bRunning);
        //_Anim.SetBool("IsJump", bJumping);
    }

    void OnlyMoveCam()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            bToggleCamRot = true;
        else
            bToggleCamRot = false;
    }

    void InputMovement()
    {
        Vector3 vMoveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        _Anim.SetBool("Walk", vMoveInput != Vector3.zero);
        bWalking = vMoveInput != Vector3.zero;

        _Anim.SetBool("Idle", !bWalking);

        if (Input.GetButton("Run") && bWalking && !Input.GetKey(KeyCode.Mouse1) && !_Anim.GetBool("Reload"))
            bRunning = true;
        else
            bRunning = false;

        fSpeed = (bRunning && !bJumping) ? fRunningSpeed : fWalkSpeed;

        Vector3 vForward = new Vector3(LookTransform.forward.x, 0f, LookTransform.forward.z).normalized;
        Vector3 vRight = new Vector3(LookTransform.right.x, 0f, LookTransform.right.z).normalized;

        Vector3 vMoveDir = vForward * vMoveInput.z + vRight * vMoveInput.x;
        
        if(bJumping)
        {
            transform.position += vMoveDir * Time.deltaTime * fSpeed;
        }
        else
        {
            m_NavMeshAgent.Move(vMoveDir * Time.deltaTime * fSpeed);
        }
    }

    void Jumping()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !bJumping)
        //{
        //    _Rigid.AddForce(Vector3.up * fJumpPower, ForceMode.Impulse);
        //    bJumping = true;
        //    m_NavMeshAgent.updatePosition = false;
        //}
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 20, Color.green);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            bJumping = false;
            m_NavMeshAgent.updatePosition = true;
            m_NavMeshAgent.Warp(transform.position);
        }
    }
}
