using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public abstract class Magic : Item
{
    [Header("Visual")]
    [SerializeField] protected Sprite _shellSprite;
    [SerializeField] protected AnimatorController _animator;
    [Header("Game")]
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _delayBetweenShots;

    protected float _lastShotTime;

    public AnimatorController Animator => _animator;
    public int Damage => _damage;
    public float DelayBetweenShots => _delayBetweenShots;

    public virtual void Init()
    {
        _lastShotTime = 0;
    }

    public abstract void Exit();

    public abstract bool TryAttack(Transform attackPoint);
}
