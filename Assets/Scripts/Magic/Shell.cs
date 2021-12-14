using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shell : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private SpriteRenderer _spriteRenderer;
    private float _speed;
    private int _damage;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    public void Init(Sprite sprite, int damage, float speed)
    {
        _spriteRenderer.sprite = sprite;
        _damage = damage;
        _speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

}
