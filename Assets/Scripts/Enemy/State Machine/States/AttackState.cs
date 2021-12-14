using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _delay;

    private float _lastAttackTime;

    private void Start()
    {
        _lastAttackTime = _delay;
    }

    private void OnEnable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Attacking, true);
        Events.InvokeAttacking(true);
    }

    private void Update()
    {
        _lastAttackTime += Time.deltaTime;

        if (_lastAttackTime >= _delay)
        {
            Attack();
            _lastAttackTime = 0;
        }
    }

    private void OnDisable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Attacking, false);
        Events.InvokeAttacking(false);
    }

    private void Attack()
    {
        Enemy.Attack(Target);
    }
}
