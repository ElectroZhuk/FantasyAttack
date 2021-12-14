using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class DeadState : State
{
    private Collider2D _collider;
    private SpriteRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Dead, true);
        Events.InvokeDead(true);
        _collider.enabled = false;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0.8f);
    }

    private void OnDisable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Dead, false);
        Events.InvokeDead(false);
        _collider.enabled = true;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1f);
    }
}
