using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Asleep : Bat_States
{
    private float aggroRange=7f;
    private static Bat_Asleep instance = null;

    public static Bat_Asleep GetInstance(Bat_Enemy self, GameObject player)
    {
        if (instance == null)
            instance = new Bat_Asleep(self, player);
        else
            instance.phase = Phase.ENTER;
        return instance;
    }
    public Bat_Asleep(Bat_Enemy self, GameObject player) : base(self,player) { }

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



}
