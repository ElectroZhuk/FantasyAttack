using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMagic : Magic
{
    [Header("Game")]
    [SerializeField] private ShadowShell _shell;

    public override void Exit()
    {
        
    }

    public override bool TryAttack(Transform attackPoint)
    {
        if (Time.realtimeSinceStartup * Time.timeScale - _lastShotTime >= _delayBetweenShots)
        {
            var shell = Instantiate(_shell, attackPoint.position, Quaternion.identity);
            shell.Init(_shellSprite, _damage, _speed);
            _lastShotTime = Time.realtimeSinceStartup * Time.timeScale;

            return true;
        }

        return false;
    }
}
