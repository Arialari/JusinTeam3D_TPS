using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Status
{
    public float fHp;
    public float fAttack;
    public float fMovementSpeed;
}

public enum MainWeap
{
    AR, SR, SG, MainWeapEnd
}
