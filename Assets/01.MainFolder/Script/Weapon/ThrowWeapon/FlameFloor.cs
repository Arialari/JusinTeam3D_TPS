using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFloor : MonoBehaviour
{
    public GameObject m_PrefabBurnEffect;
    public SoundScript m_SoundScript;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        m_SoundScript.Play_LoopOn();
        m_SoundScript.m_AudioSrc.Play(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Burn_DynamicOBJ(other);
        }
    }

    private void Burn_DynamicOBJ(Collider _DynamicOBJ)
    {
        _DynamicOBJ.GetComponent<EnemyDamageReceiver>().Start_Burn();
    }
}
