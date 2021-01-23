using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : Bat_States
{
    private static Bat_Attack instance = null;


    public static Bat_Attack GetInstance(Bat_Enemy self, GameObject player)
    {
        if (instance == null)
            instance = new Bat_Attack(self, player);
        else
            instance.phase = Phase.ENTER;
        return instance;
    }

    public Bat_Attack(Bat_Enemy self,GameObject player) : base(self,player) { }

    public override void Enter()
    {
        self.collider.size = self.AwakeColliderSize;
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
