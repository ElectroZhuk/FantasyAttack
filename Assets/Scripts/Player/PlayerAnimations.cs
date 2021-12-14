using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Attacking += OnAttack;
        _player.Hitted += OnHit;
        _player.Dead += OnDead;
    }

    private void OnDisable()
    {
        _player.Attacking -= OnAttack;
        _player.Hitted -= OnHit;
        _player.Dead -= OnDead;
    }

    private void OnAttack()
    {
        _animator.SetTrigger(PlayerAnimatorController.Params.Attack);
    }

    private void OnHit()
    {
        _animator.SetTrigger(PlayerAnimatorController.Params.Hurt);
    }

    private void OnDead()
    {
        _animator.SetBool(PlayerAnimatorController.Params.Dead, true);
    }
}
