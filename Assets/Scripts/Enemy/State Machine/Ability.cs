using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    protected Enemy Enemy;
    protected Player Player;
    protected EnemyBaseEvents Events;

    public bool CanUse { get; protected set; }
    public bool IsUsing { get; protected set; }

    public virtual event UnityAction<bool, Ability> UsingStoped;

    protected virtual void OnEnable()
    {
        IsUsing = false;
        CanUse = false;
    }

    public virtual void Init(Player player, Enemy enemy, EnemyBaseEvents events)
    {
        Enemy = enemy;
        Player = player;
        Events = events;
    }

    public virtual void Use()
    {
        IsUsing = true;
    }
}
