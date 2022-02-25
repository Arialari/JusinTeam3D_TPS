using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : Object
{
    protected GameObject gameObject;
    protected GameObject m_PrefabExplodeParticle;
    protected SoundScript m_SoundScript;
    protected GameObject m_Mesh;
    private const float m_fThrowPower = 20.0f;
    public virtual void Throw()
    {
        gameObject.transform.parent = null;
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Collider collider;
        gameObject.TryGetComponent(out collider);
        collider.isTrigger = false;
        Rigidbody rigidbody;
        gameObject.TryGetComponent(out rigidbody);
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Camera.main.transform.forward * m_fThrowPower, ForceMode.Impulse);
    }

    public virtual void PutIn()
    {

    }
    public virtual void SetPinOff(bool _bIsPinOff)
    {
        
    }

    public void SetGameObject(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }

    public void SetExplodeParticle(GameObject _particleSystem)
    {
        m_PrefabExplodeParticle = _particleSystem;
    }

    public void SetSoundScript(SoundScript _soundscript)
    {
        m_SoundScript = _soundscript;
    }

    public void SetMesh(GameObject _Mesh)
    {
        m_Mesh = _Mesh;
    }
}
