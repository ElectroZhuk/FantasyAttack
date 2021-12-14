using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        Events.InvokeRunning(true);
        //Animator.SetBool(EnemyAnimatorController.Params.Moving, true);
    }

    private void Update()
    {
        //if(Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == EnemyAnimatorController.States.Walk)
            transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.World);
    }

    private void OnDisable()
    {
        Events.InvokeRunning(false);
        //Animator.SetBool(EnemyAnimatorController.Params.Moving, false);
    }
}
