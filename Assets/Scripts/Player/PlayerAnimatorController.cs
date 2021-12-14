using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimatorController
{
    public static class Params
    {
        public const string Attack = "Attack";
        public const string Hurt = "Hurt";
        public const string Dead = "Dead";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Attack = "Attack";
        public const string CastSpell = "Cast Spell";
        public const string Die = "Dying";
        public const string Hurt = "Hurt";
    }
}
