using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainPills : Medicine
{
    public static PainPills Create(GameObject _GameObject, GameObject _Particle)
    {
        PainPills PainPills = new PainPills();
        PainPills.SetGameObject(_GameObject);
        PainPills.SetHealingParticle(_Particle);
        PainPills.Initiate();
        return PainPills;
    }

    public override void Initiate()
    {
        base.Initiate();
        m_strObjName = "¡¯≈Î¡¶";
        m_iHealingAmount = 30;
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
