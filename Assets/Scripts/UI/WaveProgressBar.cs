using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveProgressBar : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Slider _bar;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _spawner.EnemySpawned += OnEnemySpawned;
        _spawner.EnemyChanged += OnEnemyChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= OnEnemySpawned;
        _spawner.EnemyChanged -= OnEnemyChanged;
    }

    private void OnEnemySpawned(float value)
    {
        _bar.value = value;
    }

    private void OnEnemyChanged(int value)
    {
        _text.text = value.ToString();
    }
}
