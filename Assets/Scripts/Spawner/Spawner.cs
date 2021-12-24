using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemiesCountController _enemiesController;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private Dictionary<Enemy, int> _toSpawn;
    private int _toSpawnCount;
    private int _spawned;
    private float _timeSinceSpawn;
    private int _dead;

    public event UnityAction WaveDead;
    public event UnityAction<float> EnemySpawned;
    public event UnityAction<int> EnemyChanged;

    private void Start()
    {
        SetWave(_currentWaveIndex);
        _timeSinceSpawn = _currentWave.Delay;
    }

    private void OnEnable()
    {
        _enemiesController.EnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        _enemiesController.EnemyDead -= OnEnemyDead;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeSinceSpawn += Time.deltaTime;

        if (_timeSinceSpawn >= _currentWave.Delay)
        {
            var spawnedUnit = SpawnRandomUnit();
            _timeSinceSpawn = 0;

            if (spawnedUnit == null)
            {
                _currentWave = null;
            }
        }
    }

    public void NextWave()
    {
        if (_currentWaveIndex < _waves.Count - 1 && _currentWave == null && _dead == _spawned)
        {
            SetWave(++_currentWaveIndex);
            _enemiesController.BuryDeadEnemies();
        }
    }

    private void OnEnemyDead()
    {
        _dead++;
        EnemyChanged?.Invoke(_spawned - _dead);

        if (_dead == _spawned)
            WaveDead?.Invoke();
    }

    private Enemy SpawnRandomUnit()
    {
        var unitsToSpawn = _toSpawn.Where(pair => pair.Value > 0).Select(pair => pair.Key).ToArray();

        if (unitsToSpawn.Length > 0)
        {
            var unitToSpawn = unitsToSpawn[Random.Range(0, unitsToSpawn.Length - 1)];
            _enemiesController.Spawn(unitToSpawn, _spawnPoint);
            _spawned++;
            _toSpawn[unitToSpawn]--;
            EnemySpawned?.Invoke(_spawned * 1f / _toSpawnCount);

            return unitToSpawn;
        }

        return null;
    }

    private void SetWave(int index)
    {
        if (0 <= index && index < _waves.Count)
        {
            _currentWave = _waves[index];
            _toSpawn = _currentWave.GetUnitsDict();
            _toSpawnCount = 0;

            foreach (var value in _toSpawn.Values)
                _toSpawnCount += value;

            _spawned = 0;
            _dead = 0;
            EnemyChanged?.Invoke(_toSpawnCount);
        }
        else
        {
            _currentWave = null;
        }
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private List<WaveUnit> _units;
    [SerializeField] private float _delay;

    public float Delay => _delay;

    public Dictionary<Enemy, int> GetUnitsDict()
    {
        Dictionary<Enemy, int> unitsDict = new Dictionary<Enemy, int>();

        foreach (var unit in _units)
        {
            unitsDict[unit.Enemy] = unit.Count;
        }

        return unitsDict;
    }
}

[System.Serializable]
public class WaveUnit
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;

    public Enemy Enemy => _enemy;
    public int Count => _count;
}
