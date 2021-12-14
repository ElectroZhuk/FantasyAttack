using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Player Target { get; private set; }
    protected Enemy Enemy { get; private set; }

    public void Init(Player target, Enemy enemy)
    {
        Target = target;
        Enemy = enemy;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}