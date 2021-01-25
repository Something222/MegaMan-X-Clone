using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAttacking :BunnyStates
{

    private float attackTimer = 1f;
    private float xPosForBullet = .5f;
    private float yPosForBullet = .5f;
    public BunnyAttacking(BunnyEnemy self, Animator anim, PlayerCharacter player) : base(self, anim, player) { }

    public override void Enter()
    {
        anim.SetTrigger("Attacking");
        EnemyFunctions.Shoot(self.Bullet,xPosForBullet, yPosForBullet, 5, self.transform.localScale.x,self.gameObject);
        self.StartCoroutine(WaitToIdle());
        base.Enter();
    }

    private IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(attackTimer);
        nextState = new BunnyIdle(self, anim, player);
        phase = Phase.EXIT;

    }
    
    

}
