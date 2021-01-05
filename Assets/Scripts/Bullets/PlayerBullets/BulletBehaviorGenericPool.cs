using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviorGenericPool : BasicPlayerBulletBehaviour
{
    private void OnDisable()
    {
        ShotPool.Instance.ReturnToPool(this);
    }

}
