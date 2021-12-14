using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAnimatorController
{
    public static class Params
    {
        public const string Moving = "Moving";
        public const string Win = "Win";
        public const string Attacking = "Attacking";
        public const string Dead = "Dead";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Walk = "Walk";
        public const string Attack = "Attack";
        public const string Taunt = "Taunt";
        public const string Dying = "Dying";
    }
}