using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : Magic
{
    [Header("Game")]
    [SerializeField] private FireShell _shell;
    [Header("Shooting")]
    [SerializeField] private float _minControlPositionX;
    [SerializeField] private float _maxControlPositionX;
    [SerializeField] private float _minShootPositionX;
    [SerializeField] private float _maxShootPositionX;
    [SerializeField] private float _groundPositionY;
    [SerializeField] private AttackPoint _scopeTemplate;

    private AttackPoint _scope;
    private float _positionToDistanceRatio;

    public override void Init()
    {
        base.Init();
        _scope = Instantiate(_scopeTemplate, new Vector2(_maxControlPositionX, _groundPositionY), Quaternion.identity);
        _positionToDistanceRatio = Mathf.Abs(_maxShootPositionX - _minShootPositionX) / Mathf.Abs(_maxControlPositionX - _minControlPositionX);
    }

    public override void Exit()
    {
        _scope.Remove();
    }

    public override bool TryAttack(Transform attackPoint)
    {
        if (Time.realtimeSinceStartup * Time.timeScale - _lastShotTime >= _delayBetweenShots)
        {
            var shell = Instantiate(_shell, attackPoint.position, Quaternion.identity);
            var speed = _speed * (Mathf.Abs(_scope.transform.position.x - attackPoint.position.x) / Mathf.Abs(_maxShootPositionX - attackPoint.position.x));
            shell.Init(_shellSprite, _damage, speed, _scope.transform.position, _scope.Radius);
            _lastShotTime = Time.realtimeSinceStartup * Time.timeScale;

            return true;
        }

        return false;
    }

    public void MousePositionChanged(Vector2 mousePosition)
    {
        DrawScope(mousePosition);
    }

    private void DrawScope(Vector2 mousePosition)
    {
        var x = (Mathf.Clamp(mousePosition.x, _maxControlPositionX, _minControlPositionX) - _minControlPositionX) * _positionToDistanceRatio + _minShootPositionX;
        _scope.transform.position = new Vector2(x, _groundPositionY);
    }
}
