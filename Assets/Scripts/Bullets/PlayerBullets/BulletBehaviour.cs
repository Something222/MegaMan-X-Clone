using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : BasicPlayerBulletBehaviour,IGameObjectPooled
{
   
    private Pool gameObjectPool;

    public Pool GameObjectPool
    {
        get { return gameObjectPool; }

        set
        {
            if (gameObjectPool == null)
                gameObjectPool = value;
            else
                throw new System.Exception(" Set should only be used once");

        }
    }
   
  

    private void OnDisable()
    {
       // gameObjectPool.ReturnToPool(this.gameObject);
    }


}
