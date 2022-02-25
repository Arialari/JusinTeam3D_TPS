using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : Medicine
{
    public static FirstAid Create(GameObject _GameObject, GameObject _Particle)
    {
        FirstAid firstAid = new FirstAid();
        firstAid.SetGameObject(_GameObject);
        firstAid.SetHealingParticle(_Particle);
        firstAid.Initiate();
        return firstAid;
    }

    public override void Initiate()
    {
        base.Initiate();
        m_strObjName = "구급상자";
        m_iHealingAmount = 80;
        m_fUsingTime = 3.0f;
        Set_MedicineName();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ChangeTo_Parameter_ofAnimator(Animator _Animator, bool _bParam)
    {
        base.ChangeTo_Parameter_ofAnimator(_Animator, _bParam);
        _Animator.SetBool("UseOBJ", _bParam);
    }

    public void Set_MedicineName()
    {
        base.Set_MedicineName(m_strObjName);
    }
}
