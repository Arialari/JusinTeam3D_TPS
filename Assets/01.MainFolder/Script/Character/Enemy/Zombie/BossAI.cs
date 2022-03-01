using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public enum BossPattern
    {
        Pattern_Move01, Pattern_Att01, Pattern_Att02, Pattern_Charge, Pattern_Crush, Pattern_Kneeling, Pattern_Angry, Pattern_End
    }

    public BossPattern m_eBossPattern { get; set; }
    private BossPattern m_ePrevBossPattern;
    public Transform m_playerTransform { get; set; }
    private Transform m_TargetTransform;
    private Animator m_Animator;
    public NavMeshAgent m_NavMeshAgent;
    public EnemyStatus m_EnemyStatus;
    public Canvas m_HP_Canavs;

    public Vector3 m_vDir;
    public bool m_bAngry = false;

    private float m_fThrowCoolTime = 0f;
    private float m_fChargeCoolTime = 0f;
    private float m_fChargingTime = 0f;
    private float m_fKneelingTime = 0f;

    private float m_fMaxHp = 0f;
    public bool m_bThrow = false;
    public bool m_bAttack = false;
    public bool m_bAlert = false;
    public bool m_bCrush = false;
    public bool m_bCharge = false;

    public int m_iAtkAudioClip = 0;

    private Image m_ImgHP_Bar;
    private Text m_txtHP_text;

    public GameObject m_RockSocket;
    public GameObject m_RockPrefab;
    public GameObject m_InstanceRock { get; set; }

    public float m_fWalkSpeed;

    public SoundScript m_SoundScript { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        m_playerTransform = GameObject.Find("Player").transform;
        TryGetComponent(out m_NavMeshAgent);
        m_Animator = transform.GetChild(0).GetComponent<Animator>();
        m_fWalkSpeed = m_NavMeshAgent.speed;
        m_EnemyStatus = GetComponent<EnemyStatus>();
        m_SoundScript = GetComponent<SoundScript>();

        m_eBossPattern = BossPattern.Pattern_Move01;
        m_ePrevBossPattern = BossPattern.Pattern_End;
        m_fMaxHp = m_EnemyStatus.m_Status.fHp;

        m_ImgHP_Bar = m_HP_Canavs.transform.GetTransformInAllChildren("HP_Bar").GetComponent<Image>();
        m_txtHP_text = m_HP_Canavs.transform.GetTransformInAllChildren("HP_txt").GetComponent<Text>();

        m_HP_Canavs.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bAlert)
            return;

        if (m_bAngry)
        {
            if ((!m_bCharge || !m_bCrush))
            {
                m_Animator.speed = 2.0f;
            }

            else
            {
                m_Animator.speed = 1.0f;
            }
        }

        if (!m_bCharge || !m_bCrush)
        {
            m_fThrowCoolTime += Time.deltaTime;
            m_fChargeCoolTime += Time.deltaTime;
        }

        m_vDir = m_playerTransform.position - transform.position;
        
        AttackPattern();
        Pattern_Logic();

        if (m_EnemyStatus.m_Status.fHp <= 0f)
        {
            m_Animator.speed = 0.8f;
            m_NavMeshAgent.speed = 0f;
        }

        HP_UI_Logic();

        RandomFnc();
    }

    private void Pattern_Logic()
    {
        switch (m_eBossPattern)
        {
            case BossPattern.Pattern_Move01:

                m_Animator.SetBool("Idle", false);
                m_Animator.SetBool("Walk", true);
                
                if (m_Animator.GetBool("Walk"))
                    m_NavMeshAgent.SetDestination(m_playerTransform.position);
                
                break;

            case BossPattern.Pattern_Att01:
                
                if (!m_bAttack)
                {
                    m_bAttack = true;
                    m_NavMeshAgent.isStopped = true;
                    m_Animator.SetTrigger("Attack");
                }

                break;

            case BossPattern.Pattern_Att02:

                if (m_eBossPattern != m_ePrevBossPattern)
                    ThrowPattern();

                break;

            case BossPattern.Pattern_Charge:

                m_Animator.SetBool("Walk", false);

                if (m_eBossPattern != m_ePrevBossPattern)
                    ChargePattern();

                    if (m_fChargingTime >= 5f)
                    {
                        m_fChargingTime = 0f;
                        m_Animator.SetTrigger("ChargeStop");
                        m_bCharge = false;
                    }
                    else
                    {
                        m_fChargingTime += Time.deltaTime;
                        Charge_Run();
                    }

                break;

            case BossPattern.Pattern_Crush:

                m_fChargingTime = 0f;
                m_Animator.SetBool("Crush", true);

                break;

            case BossPattern.Pattern_Kneeling:

                m_fKneelingTime += Time.deltaTime;

                if (m_bAngry && m_fKneelingTime >= 2f)
                {
                    StandUp_from_Kneeling();
                }

                else if (m_fKneelingTime >= 3f)
                { 
                    StandUp_from_Kneeling();
                }

                break;

            case BossPattern.Pattern_Angry:

                Angry();
                
                break;
        }
        m_ePrevBossPattern = m_eBossPattern;
    }

    private void ThrowPattern()
    {
        m_NavMeshAgent.isStopped = true;
        m_bThrow = true;
        m_InstanceRock = Instantiate(m_RockPrefab, m_RockSocket.transform);
        m_fThrowCoolTime = 0f;
        m_Animator.SetBool("Throw", true);
    }

    private void ChargePattern()
    {
        m_NavMeshAgent.isStopped = true;
        m_fChargeCoolTime = 0f;
        m_Animator.SetTrigger("ChargeStart");
    }

    private void Charge_Run()
    {
        m_Animator.SetBool("Walk", false);

        if (m_bAngry)
        {
            transform.position += transform.forward * Time.deltaTime * 20f;
            return;
        }

        transform.position += transform.forward * Time.deltaTime * 15f;
    }

    private void StandUp_from_Kneeling()
    {
        m_fKneelingTime = 0f;
        m_Animator.SetBool("Crush", false);
        m_Animator.SetTrigger("StandUp");
        //m_NavMeshAgent.updateRotation = true;
       // m_NavMeshAgent.updatePosition = true;
    }

    private void AttackPattern()
    {
        if (m_bThrow || m_bAttack || m_bCrush || m_bCharge || m_Animator.GetBool("Crush"))
            return;

        if (6.0f >= m_vDir.magnitude)
        {
            m_eBossPattern = BossPattern.Pattern_Att01;
        }
        else if (m_fThrowCoolTime >= 12.0f && 25.0f <= m_vDir.magnitude)
        {
            m_eBossPattern = BossPattern.Pattern_Att02;
        }
        else if (m_fChargeCoolTime >= 20.0f && 20.0f <= m_vDir.magnitude)
        {
            m_bCharge = true;
            m_eBossPattern = BossPattern.Pattern_Charge;
        }
        else if (!m_bAngry && m_EnemyStatus.m_Status.fHp <= m_fMaxHp / 2)
        {
            m_eBossPattern = BossPattern.Pattern_Angry;
        }
        else
        {
            m_eBossPattern = BossPattern.Pattern_Move01;
        }
    }

    public void Die()
    {
        ExtractionQuestManager.GetInstance().ToNextQuest(ExtractionQuestManager.Mission.Boss_Fight);
        Destroy(gameObject);
    }

    public void Angry()
    {
        m_bAngry = true;
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.angularSpeed = 150f;
        m_Animator.SetTrigger("Alet");
    }

    public void Alert()
    {
        m_bAlert = true;
        m_HP_Canavs.enabled = true;
    }

    private void HP_UI_Logic()
    {
        m_ImgHP_Bar.fillAmount = 1.0f / m_fMaxHp * m_EnemyStatus.m_Status.fHp;
        m_txtHP_text.text = string.Format("{0:n0}", m_EnemyStatus.m_Status.fHp) + string.Format(" / {0:n0}", m_fMaxHp);
    }

    public void RandomFnc()
    {
        m_iAtkAudioClip = Random.Range(3, 5);
    }
}
