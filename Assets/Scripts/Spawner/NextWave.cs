using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Spawner _spawner;


    private void Start()
    {
        _button.interactable = false;
    }

    private void OnEnable()
    {
        _spawner.WaveDead += ActivateButton;
        _button.onClick.AddListener(SpawnNextWave);
    }

    private void OnDisable()
    {
        _spawner.WaveDead -= ActivateButton;
        _button.onClick.RemoveListener(SpawnNextWave);
    }

    private void ActivateButton()
    {
        _button.interactable = true;
    }

    private void SpawnNextWave()
    {
        _spawner.NextWave();
        _button.interactable = false;
    }
}
