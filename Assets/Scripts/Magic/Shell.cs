using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Shell : MonoBehaviour
{
    [SerializeField] protected float _rotationSpeed;

    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider;
    protected float _speed;
    protected int _damage;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public virtual void Init(Sprite sprite, int damage, float speed)
    {
        _spriteRenderer.sprite = sprite;
        _damage = damage;
        _speed = speed;
    }
}
