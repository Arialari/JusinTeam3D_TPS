using System.Collections.Generic;
using UnityEngine;

public class AIViewPerception : MonoBehaviour
{
    private GameObject m_Player;
    private IAlertEventReceiver m_AlertEventReceiver;

    private static bool bUpdate = false;

    public bool IsActive = true;
    public float radius = 10f;    
    public float angel = 120f;
    private const float m_fRadius = 50f;

    public enum Alert { Safe, Awake, Alert }
    public Alert m_eAlert { get; set; }

    //Results of the reconnaissance
    public GameObject PerceptionResult { get; private set; }

    public void Check(GameObject explorer, GameObject target)
    {
        PerceptionResult = null;

        //For distance judgment, the square is used here to reduce the calculation amount of the system root sign
        Vector3 offset = target.transform.position - explorer.transform.position;
        if (offset.sqrMagnitude > radius * radius)
            return;

        //Angle judgment, whether it is within sight
        float angel = Vector3.Angle(explorer.transform.forward, offset.normalized);
        if (angel > this.angel * 1.0f / 2)
            return;

        //Ray judgment, whether it is blocked by other targets or obstacles
        Ray ray = new Ray(explorer.transform.position, offset);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, radius))
        {
            if (info.collider.gameObject == target)
            {
                PerceptionResult = target;
                if(Alert.Safe == m_eAlert)
                {
                    m_eAlert = Alert.Awake;
                    m_AlertEventReceiver.AlertAwaken();
                }
            }
        }

    }

    void Start()
    {
        m_Player = GameObject.Find("Player");
        if (!TryGetComponent(out m_AlertEventReceiver))
        {
            Debug.LogWarning(ToString() + ".Start() Cannot found AlertEventReceiver");
        }
        EnemyManager.GetInstance().AddAIViewPerception(this);
    }

    void Update()
    {
        if (!IsActive)
            return;
        if(m_eAlert == Alert.Safe)
        {
            Check(gameObject, m_Player);
        }
        else if (m_eAlert == Alert.Alert)
        {
            if(!bUpdate)
            {
                UpdateAlertAround();
                bUpdate = true;
            }
        }
    }

    private void LateUpdate()
    {
        bUpdate = false;
    }
    private void UpdateAlertAround()
    {
        if (Alert.Alert == m_eAlert)
        {
            // Enemy Alert
            Collider[] colliders;
            colliders = Physics.OverlapSphere(m_Player.transform.position, m_fRadius, LayerMask.GetMask("Enemy"));
            foreach( Collider collider in colliders)
            {
                AIViewPerception aIViewPerception;                
                aIViewPerception = collider.GetComponentInParent<AIViewPerception>();
                if( aIViewPerception )
                {
                    aIViewPerception.m_eAlert = Alert.Alert;
                }
                Animator animator;
                animator = collider.GetComponentInParent<Animator>();
                if( animator )
                {
                    animator.SetBool("IsAlert", true);
                }
            }

            //Spawner Alert
            colliders = Physics.OverlapSphere(m_Player.transform.position, m_fRadius * 2f, LayerMask.GetMask("EnemySpawner"));
            foreach (Collider collider in colliders)
            {
                EnemySpawner enemySpawner;
                collider.TryGetComponent(out enemySpawner);
                if (enemySpawner)
                {
                    enemySpawner.m_eAlert = Alert.Alert;
                }
            }
        }
    }
}