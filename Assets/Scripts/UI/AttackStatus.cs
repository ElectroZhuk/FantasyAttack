using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackStatus : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _magicIcon;
    [SerializeField] private Image _fill;

    private Magic _magic;

    private void OnEnable()
    {
        _player.MagicChanged += OnMagicChanged;
        _player.Attacking += OnAttacking;
    }

    private void OnDisable()
    {
        _player.MagicChanged -= OnMagicChanged;
        _player.Attacking -= OnAttacking;
    }

    private void OnMagicChanged(Magic magic)
    {
        _magicIcon.sprite = magic.Icon;
        _magic = magic;
    }

    private void OnAttacking()
    {
        StartCoroutine(ShowReload(_magic.DelayBetweenShots));
    }

    private IEnumerator ShowReload(float timeToReload)
    {
        float elapsedTime = 0;
        _fill.fillAmount = 1;

        while (elapsedTime < timeToReload)
        {
            _fill.fillAmount = 1 - elapsedTime / timeToReload;

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        _fill.fillAmount = 0;
    }
}
