using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{
    public EnemiesCountController EnemiesController { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    public void Init(Player player, EnemiesCountController enemiesController)
    {
        base.Init(player);
        EnemiesController = enemiesController;
    }
}
