using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : Bat_States
{

    public Bat_Attack(Bat_Enemy self) : base(self) { }

    public override void Enter()
    {
        self.moveSpeed = self.attackMoveSpeed;
        self.Anim.SetBool("Attacking", true);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        self.transform.position=Vector2.MoveTowards(currPos, playerPos, self.moveSpeed * Time.deltaTime);
    }

    

}
