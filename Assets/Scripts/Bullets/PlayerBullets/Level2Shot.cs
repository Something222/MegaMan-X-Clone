using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Shot : ChargeShot
{
    protected override void OnDisable()
    {
        base.OnDisable();
        Level2ShotPool.Instance.ReturnToPool(this);
    }


}
