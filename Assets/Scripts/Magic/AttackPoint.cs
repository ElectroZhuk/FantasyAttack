using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AttackPoint : MonoBehaviour
{
    [SerializeField] private float _raduisRatio = 1f;

    private SpriteRenderer _renderer;

    public float Radius { get; private set; }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        transform.localScale = transform.localScale * _raduisRatio;
        Radius = Mathf.Abs(_renderer.sprite.rect.width / _renderer.sprite.pixelsPerUnit * transform.localScale.x / 2);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
