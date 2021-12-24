using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    private float _speed;

    protected override void Awake()
    {
        base.Awake();
        StateEvent = Events.InvokeRunning;
    }

    private void OnEnable()
    {
        Events.InvokeRunning(true);
        //_speed = Enemy.Speed;
        //Animator.SetBool(EnemyAnimatorController.Params.Moving, true);
    }

    protected override void Update()
    {
        base.Update();
        //if(Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == EnemyAnimatorController.States.Walk)
            transform.Translate(Vector2.right * Enemy.Speed * Time.deltaTime, Space.World);
    }

    private void OnDisable()
    {
        Events.InvokeRunning(false);
        //Animator.SetBool(EnemyAnimatorController.Params.Moving, false);
    }
}
