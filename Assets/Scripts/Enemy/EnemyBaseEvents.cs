using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBaseEvents : MonoBehaviour
{
    public event UnityAction<bool> Running;
    public event UnityAction<bool> Attacking;
    public event UnityAction<bool> Taunting;
    public event UnityAction<bool> Dead;

    public void InvokeRunning(bool isActive)
    {
        Running?.Invoke(isActive);
    }

    public void InvokeAttacking(bool isActive)
    {
        Attacking?.Invoke(isActive);
    }

    public void InvokeTaunting(bool isActive)
    {
        Taunting?.Invoke(isActive);
    }

    public void InvokeDead(bool isActive)
    {
        Dead?.Invoke(isActive);
    }
}
