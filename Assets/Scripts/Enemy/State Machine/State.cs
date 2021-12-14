using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseEvents))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target;
    protected Enemy Enemy;
    protected EnemyBaseEvents Events;

    protected virtual void Awake()
    {
        Events = GetComponent<EnemyBaseEvents>();
    }

    public virtual void Enter(Player target, Enemy enemy)
    {
        if (enabled == false)
        {
            enabled = true;
            Enemy = enemy;
            Target = target;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(target, enemy);
            }
        }
    }

    public virtual void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
