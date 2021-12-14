using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntState : State
{
    private void OnEnable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Win, true);
        Events.InvokeTaunting(true);
    }

    private void OnDisable()
    {
        //Animator.SetBool(EnemyAnimatorController.Params.Win, false);
        Events.InvokeTaunting(false);
    }
}
