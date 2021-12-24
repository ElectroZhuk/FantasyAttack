using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseEvents))]
public abstract class State : MonoBehaviour
{
    [SerializeField] protected List<Ability> Abilities;
    [SerializeField] protected List<Transition> Transitions;

    protected Player Target;
    protected Enemy Enemy;
    protected EnemyBaseEvents Events;
    protected Action<bool> StateEvent;
    protected Ability UsingAbility;

    protected virtual void Awake()
    {
        Events = GetComponent<EnemyBaseEvents>();
    }

    protected virtual void Update()
    {
        foreach (var ability in Abilities)
        {
            if (ability.CanUse)
            {
                SwitchAllActivity(false);
                ability.Use();
                ability.UsingStoped += SwitchAllActivity;
            }
        }
    }

    private void SwitchAllActivity(bool switchTo, Ability source = null)
    {
        if (source != null)
        {
            source.UsingStoped -= SwitchAllActivity;
        }

        StateEvent(switchTo);

        foreach (var transition in Transitions)
            transition.enabled = switchTo;

        enabled = switchTo;
    }

    public virtual void Enter(Player target, Enemy enemy)
    {
        if (enabled == false)
        {
            enabled = true;
            Enemy = enemy;
            Target = target;

            foreach (var transition in Transitions)
            {
                transition.enabled = true;
                transition.Init(target, enemy);
            }

            foreach (var ability in Abilities)
            {
                ability.enabled = true;
                ability.Init(target, enemy, Events);
            }
        }
    }

    public virtual void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in Transitions)
                transition.enabled = false;

            foreach (var ability in Abilities)
                ability.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in Transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
