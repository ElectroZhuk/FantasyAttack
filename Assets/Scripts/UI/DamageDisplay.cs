using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.DamageChanged += OnDamageChanged;
    }

    private void OnDisable()
    {
        _player.DamageChanged -= OnDamageChanged;
    }

    private void OnDamageChanged(int value)
    {
        _text.text = value.ToString();
    }
}
