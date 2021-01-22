using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyIdle : BunnyStates
{
    private float idleTimer = 1f; //How long will he be in idle state
  
    public BunnyIdle(BunnyEnemy self, Animator anim,PlayerCharacter player) : base(self, anim,player) { }

    public override void Enter()
    {
        anim.SetBool("Idle", true);
        self.StartCoroutine(Wait());
        base.Enter();

    }

    public override void Update()
    {
        TrackPlayer();
    }

    public override void Exit()
    {
        anim.SetBool("Idle", false);
        base.Exit();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(idleTimer);
        if (Random.Range(0, 1) == 1)
            nextState = new BunnyAttacking(self, anim, player);
        else
            nextState = new BunnyMoving(self, anim, player);

        //testing 
        nextState = new BunnyAttacking(self, anim, player);
        phase = Phase.EXIT;
    }
    private void TrackPlayer()
    {
        if (player.transform.position.x < self.gameObject.transform.position.x)
            self.gameObject.transform.localScale = new Vector2(1, 1);
        else
            self.gameObject.transform.localScale = new Vector2(-1, 1);
    }


}
