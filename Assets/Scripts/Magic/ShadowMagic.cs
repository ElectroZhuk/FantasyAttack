using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMagic : Magic
{
    public override void Attack(Transform position)
    {
        var shell = Instantiate(_shell, position.position, Quaternion.identity);
        shell.Init(_shellSprite, _damage, _speed);
    }
}
