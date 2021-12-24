using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class EnemiesCountController : MonoBehaviour
{
    [SerializeField] Player _player;

    private List<Enemy> _aliveEnemies;
    private List<Enemy> _deadEnemies;
    private List<Enemy> _enemyPull;

    public event UnityAction EnemyDead;

    private void Awake()
    {
        _aliveEnemies = new List<Enemy>();
        _deadEnemies = new List<Enemy>();
        _enemyPull = new List<Enemy>();
    }

    public void Spawn(Enemy enemyToSpawn, Transform point)
    {
        var availableEnemy = _enemyPull.Where(enemy => enemy.GetType() == enemyToSpawn.GetType()).Select(enemy => enemy).ToArray();

        if (availableEnemy.Length > 0)
        {
            var enemy = availableEnemy.FirstOrDefault();
            enemy.Respawn();
            enemy.transform.position = new Vector3(point.position.x, point.position.y, enemy.transform.position.z);
            enemy.gameObject.SetActive(true);
            enemy.Dead += OnEnemyDead;
            _enemyPull.Remove(enemy);
            _aliveEnemies.Add(enemy);
        }
        else
        {
            var enemy = Instantiate(enemyToSpawn, new Vector3(point.position.x, point.position.y, point.position.z + 0.001f * CountAllEnemies()), Quaternion.identity, point).GetComponent<Enemy>();
            enemy.name += CountAllEnemies().ToString();

            if (enemy is Ogre ogre)
                ogre.Init(_player, this);
            else
                enemy.Init(_player);

            enemy.Dead += OnEnemyDead;
            _aliveEnemies.Add(enemy);
        }
    }

    private int CountAllEnemies()
    {
        return _enemyPull.Count + _aliveEnemies.Count + _deadEnemies.Count;
    }

    public int CountSpawnedEnemies()
    {
        return _aliveEnemies.Count + _deadEnemies.Count;
    }

    public int CountSpawnedEnemies<T>()
    {
        int alive = _aliveEnemies.Select(enemy => enemy).Where(enemy => enemy is T).Count();
        int dead = _deadEnemies.Select(enemy => enemy).Where(enemy => enemy is T).Count();

        return dead + alive;
    }

    public int CountAliveEnemies()
    {
        return _aliveEnemies.Count;
    }

    public int CountAliveEnemies<T>()
    {
        return _aliveEnemies.Select(enemy => enemy).Where(enemy => enemy is T).Count();
    }

    public int CountDeadEnemies()
    {
        return _deadEnemies.Count;
    }

    public void BuryDeadEnemies()
    {
        foreach (var deadEnemy in _deadEnemies)
            deadEnemy.Bury();
    }

    private void OnEnemyDead()
    {
        var deadEnemy = _aliveEnemies.FirstOrDefault(enemy => enemy.Health <= 0);

        if (deadEnemy != null)
        {
            deadEnemy.Dead -= OnEnemyDead;
            EnemyDead?.Invoke();
            _aliveEnemies.Remove(deadEnemy);
            _deadEnemies.Add(deadEnemy);
            deadEnemy.Buried += OnEnemyBuried;
        }
    }

    private void OnEnemyBuried()
    {
        var buriedEnemy = _deadEnemies.FirstOrDefault(enemy => enemy.IsBuried);

        if (buriedEnemy != null)
        {
            buriedEnemy.Buried -= OnEnemyBuried;
            _deadEnemies.Remove(buriedEnemy);
            _enemyPull.Add(buriedEnemy);
        }
    }

    public Enemy GetNearesAliveEnemy(Enemy fromEnemy, bool isTargeted = false)
    {
        Enemy nearestEnemy = null;

        foreach (var enemy in _aliveEnemies)
        {
            if (enemy.GetType() != fromEnemy.GetType() && enemy.IsTargeted == isTargeted && (nearestEnemy == null || Vector2.Distance(enemy.transform.position, fromEnemy.transform.position) < Vector2.Distance(nearestEnemy.transform.position, fromEnemy.transform.position)))
                nearestEnemy = enemy;
        }
        return nearestEnemy;
    }
}
