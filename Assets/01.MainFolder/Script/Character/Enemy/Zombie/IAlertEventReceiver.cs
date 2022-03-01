using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlertEventReceiver
{
    public void AlertAwaken();
    public void AlertSafe();
}
