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
    [SerializeField] protected Shell _shell;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;

    public bool IsBought { get; protected set; }

    public abstract void Attack(Transform position);

    public override bool CanSell()
    {
        return IsBought == false;
    }

    public override void Sell()
    {
        if (CanSell())
            IsBought = true;
    }
}
