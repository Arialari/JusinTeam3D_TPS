using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenalin : Medicine
{
    public static Adrenalin Create(GameObject _GameObject, GameObject _Particle)
    {
        Adrenalin Adrenalin = new Adrenalin();
        Adrenalin.SetGameObject(_GameObject);
        Adrenalin.SetHealingParticle(_Particle);
        Adrenalin.Initiate();
        return Adrenalin;
    }

    public override void Initiate()
    {
        base.Initiate();
        m_strObjName = "아드레날린 주사기";
        m_iHealingAmount = 80;
        m_fUsingTime = 2.0f;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeTo_Parameter_ofAnimator(Animator _Animator, bool _bParam)
    {
        base.ChangeTo_Parameter_ofAnimator(_Animator, _bParam);
        _Animator.SetBool("PainPill", _bParam);
    }
}
