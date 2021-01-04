using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Asleep : Bat_States
{
    private float aggroRange=7f;

    public Bat_Asleep(Bat_Enemy self) : base(self) { }

    public override void Enter()
    {
        self.collider.size = self.AsleepColliderSize;
        self.anim.SetBool("Attacking", false);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
      
        if (Vector2.Distance(playerPos, currPos) <= aggroRange)
        {
            SwitchToAttackPhase();
        }
     
    }

    private void SwitchToAttackPhase()
    {

        nextState = new Bat_Attack(self);
        phase = Phase.EXIT;
    }

}
