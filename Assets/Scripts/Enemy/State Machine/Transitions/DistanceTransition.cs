using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _distance;
    [SerializeField] private float _spreadPercent;

    protected override void OnEnable()
    {
        base.OnEnable();
        float minDistance = _distance * (1 - (_spreadPercent / 100));
        float maxDistance = _distance * (1 + (_spreadPercent / 100));
        _distance = Random.Range(minDistance, maxDistance);
    }

    private void Update()
    {
        if (Vector2.Distance(Enemy.transform.position, Target.transform.position) <= _distance)
        {
            NeedTransit = true;
        }
    }
}
