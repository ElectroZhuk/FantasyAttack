using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : Shell
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Transform _maxHeightPoint;
    [SerializeField] private Explosion _explosion;

    private Vector2 _startPointPosition;
    private Vector2 _finishPointPosition;
    private float _height;
    private float _width;
    private float _explosionRadius;

    public void Init(Sprite sprite, int damage, float speed, Vector2 finishPointPosition, float explosionRadius)
    {
        base.Init(sprite, damage, speed);
        _startPointPosition = transform.position;
        _finishPointPosition = finishPointPosition;
        _explosionRadius = explosionRadius;
        _height = Mathf.Abs(_maxHeightPoint.transform.position.y - _finishPointPosition.y);
        _width = Mathf.Abs(_finishPointPosition.x - _startPointPosition.x);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float y = (_height * _curve.Evaluate(Mathf.Abs(transform.position.x - _startPointPosition.x) / _width) + _finishPointPosition.y) - transform.position.y;

        while (Mathf.Abs(transform.position.x - _startPointPosition.x) / Mathf.Abs(_finishPointPosition.x - _startPointPosition.x) < 1)
        {
            transform.Translate(new Vector2(-1 * _speed * Time.deltaTime, y), Space.World);
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _speed);
            y = (_height * _curve.Evaluate(Mathf.Abs(transform.position.x - _startPointPosition.x) / _width) + _finishPointPosition.y) - transform.position.y;

            yield return null;
        }

        Explose();
    }

    private void Explose()
    {
        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.Init(_damage, _explosionRadius);
        Destroy(gameObject);
    }
}
