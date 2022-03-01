using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : ThrowWeapon
{
    public bool m_bDestroy = false;
    private GameObject m_CopyParticle;

    public static Molotov Create(GameObject _gameObject, GameObject _Particle, SoundScript _soundscript, GameObject _Mesh)
    {
        Molotov molotov = new Molotov();
        molotov.SetGameObject(_gameObject);
        molotov.SetExplodeParticle(_Particle);
        molotov.SetSoundScript(_soundscript);
        molotov.SetMesh(_Mesh);
        return molotov;
    }
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (m_bDestroy && !m_CopyParticle)
        {
            Destroy(gameObject);
        }
    }

    public override void Throw()
    {
        base.Throw();
    }

    public void DestroyBottle()
    {
        if (!m_bDestroy)
        {
            m_bDestroy = true;
            m_CopyParticle = Instantiate(m_PrefabExplodeParticle, gameObject.transform.position, Quaternion.Euler(-90f, 0, 0));
            m_SoundScript.Play_JustOne(0);
            m_Mesh.SetActive(false);
        }
    }
}
