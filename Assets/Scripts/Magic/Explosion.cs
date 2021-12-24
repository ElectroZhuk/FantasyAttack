using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;

    private int _damage;
    private List<Enemy> _damagedEnemies;

    public void Init(int damage, float radius)
    {
        _damage = damage;
        float ration = radius / (_renderer.sprite.bounds.extents.x);
        transform.localScale = new Vector3(transform.localScale.x * ration, transform.localScale.y * ration, transform.localScale.z);
        _damagedEnemies = new List<Enemy>();
        _animator.SetTrigger(ExplosionAnimatorController.Params.Explose);
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        while (_animator.IsInTransition(0) == false)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy;

        if (collision.TryGetComponent<Enemy>(out enemy) && _damagedEnemies.Contains(enemy) == false)
        {
            enemy.TakeDamage(_damage);
            _damagedEnemies.Add(enemy);
        }
    }
}
