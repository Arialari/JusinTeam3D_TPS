using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBomb : ThrowWeapon
{
    public static PipeBomb Create(GameObject _gameObject, GameObject _Particle)
    {
        PipeBomb pipebomb = new PipeBomb();
        pipebomb.SetGameObject(_gameObject);
        pipebomb.SetExplodeParticle(_Particle);
        return pipebomb;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public override void Throw()
    {
        base.Throw();
    }

    public void SetTickOn()
    {

    }
}
