﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Shot : ChargeShot
{

    protected override void OnDisable()
    {
        base.OnDisable();
        Level1ShotPool.Instance.ReturnToPool(this);

    }
}