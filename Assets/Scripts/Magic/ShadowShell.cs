using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShell : Shell
{   
    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
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
