using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceIncreasedTransition : Transition
{
    [SerializeField] private float _distance;

    private void Update()
    {
        if (Vector2.Distance(Enemy.transform.position, Target.transform.position) > _distance)
        {
            NeedTransit = true;
        }
    }
}
