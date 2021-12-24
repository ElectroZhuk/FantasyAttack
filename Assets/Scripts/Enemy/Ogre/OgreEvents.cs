using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OgreEvents : EnemyBaseEvents
{
    public event UnityAction Boosting;
    public event UnityAction Jumping;

    public void InvokeBoosting()
    {
        Boosting?.Invoke();
    }

    public void InvokeJumping()
    {
        Jumping?.Invoke();
    }
}
