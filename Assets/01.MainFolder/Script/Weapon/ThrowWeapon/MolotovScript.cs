using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovScript : MonoBehaviour, IThrowWeaponScript
{
    public GameObject m_PrefabFlameFloor;
    private Molotov m_Molotov;
    public SoundScript m_SoundScript;
    public GameObject m_Mesh;

    // Start is called before the first frame update
    void Awake()
    {
        m_Molotov = Molotov.Create(gameObject, m_PrefabFlameFloor, m_SoundScript, m_Mesh);
    }

    // Update is called once per frame
    void Update()
    {
        m_Molotov.Update();
    }

    public ThrowWeapon GetThrowWeapon()
    {
        return m_Molotov;
    }

    public void OnCollisionEnter(Collision collision)
    {
        m_Molotov.DestroyBottle();
    }
}
