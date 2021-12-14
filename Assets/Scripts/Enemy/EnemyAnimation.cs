using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyBaseEvents _events;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _events.Running += OnRunning;
        _events.Attacking += OnAttacking;
        _events.Taunting += OnTaunting;
        _events.Dead += OnDead;
    }

    private void OnDisable()
    {
        _events.Running -= OnRunning;
        _events.Attacking -= OnAttacking;
        _events.Taunting -= OnTaunting;
        _events.Dead -= OnDead;
    }

    private void OnRunning(bool status)
    {
        _animator.SetBool(EnemyAnimatorController.Params.Moving, status);
    }

    private void OnAttacking(bool status)
    {
        _animator.SetBool(EnemyAnimatorController.Params.Attacking, status);
    }

    private void OnTaunting(bool status)
    {
        _animator.SetBool(EnemyAnimatorController.Params.Win, status);
    }

    private void OnDead(bool status)
    {
        _animator.SetBool(EnemyAnimatorController.Params.Dead, status);
    }
}
