using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeScript : MonoBehaviour, IThrowWeaponScript
{
    public GameObject m_PrefabExplodeParticle;
    private FragGrenade fragGrenade;
    public SoundScript m_SoundScript;
    public GameObject m_Mesh;

    void Awake()
    {
        fragGrenade = FragGrenade.Create(gameObject, m_PrefabExplodeParticle, m_SoundScript, m_Mesh);
    }
    void Start()
    {
        fragGrenade.Start();
    }

    // Update is called once per frame
    void Update()
    {
        fragGrenade.Update();
    }
    public ThrowWeapon GetThrowWeapon()
    {
        return fragGrenade;
    }
}
