using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadTransition : Transition
{
    private void Update()
    {
        if (Target.Health <= 0)
            NeedTransit = true;
    }
}
