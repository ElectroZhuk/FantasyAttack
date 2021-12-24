using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoostAbility : Ability
{
    [SerializeField] [Range(0, 100)] private float _enemiesAlivePercent;
    [SerializeField] private float _delayBeetwenBoosts;
    [SerializeField] private float _boostRatio;
    [SerializeField] private float _boostDuration;
    [SerializeField] private float _boostEnemyDistance;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private AnimationCurve _jumpCurve;

    private Ogre _enemy;
    private OgreEvents _events;
    private float _lastBoostTime;

    public override event UnityAction<bool, Ability> UsingStoped;

    private void Awake()
    {
        _lastBoostTime = -_delayBeetwenBoosts;
    }

    public override void Init(Player player, Enemy enemy, EnemyBaseEvents events)
    {
        base.Init(player, enemy, events);

        if (Enemy is Ogre ogre)
            _enemy = ogre;

        if (Events is OgreEvents ogreEvents)
            _events = ogreEvents;

    }

    private void Update()
    {
        if (_enemy != null && (float)(_enemy.EnemiesController.CountAliveEnemies() - _enemy.EnemiesController.CountAliveEnemies<Ogre>()) / (_enemy.EnemiesController.CountSpawnedEnemies() - _enemy.EnemiesController.CountSpawnedEnemies<Ogre>()) <= _enemiesAlivePercent / 100f && Time.realtimeSinceStartup * Time.timeScale - _lastBoostTime >= _delayBeetwenBoosts && _enemy.EnemiesController.GetNearesAliveEnemy(_enemy) != null)
        {
            CanUse = true;
        }
        else
            CanUse = false;
    }

    public override void Use()
    {
        if (CanUse && IsUsing == false)
        {
            IsUsing = true;
            TryBoost(_enemy.EnemiesController.GetNearesAliveEnemy(_enemy));
        }
    }

    private void TryBoost(Enemy enemy)
    {
        enemy.IsTargeted = true;

        if (Vector2.Distance(transform.position, enemy.transform.position) <= _boostEnemyDistance)
            Boost(enemy);
        else
            StartCoroutine(JumpToTarget(enemy));
    }

    private void Boost(Enemy enemy)
    {
        _events.InvokeBoosting();
        enemy.BoostSpeed(_boostRatio, _boostDuration);
        IsUsing = false;
        UsingStoped?.Invoke(true, this);
        _lastBoostTime = Time.realtimeSinceStartup;
        enemy.IsTargeted = false;
    }

    private IEnumerator JumpToTarget(Enemy target)
    {
        float directionX = Mathf.Sign(target.transform.position.x - transform.position.x);
        Vector2 startPosition = transform.position;
        Vector2 finishPosition = target.transform.position;
        float width = Mathf.Abs(target.transform.position.x - transform.position.x);
        float y = (_jumpHeight * _jumpCurve.Evaluate(Mathf.Abs(transform.position.x - startPosition.x) / width) + finishPosition.y) - transform.position.y;
        _events.InvokeJumping();

        while (Mathf.Abs(transform.position.x - startPosition.x) / Mathf.Abs(finishPosition.x - startPosition.x) < 1)
        {
            transform.Translate(new Vector2(directionX * _jumpSpeed * Time.deltaTime, y), Space.World);
            y = (_jumpHeight * _jumpCurve.Evaluate(Mathf.Abs(transform.position.x - startPosition.x) / width) + finishPosition.y) - transform.position.y;

            yield return null;
        }

        Boost(target);
    }
}
