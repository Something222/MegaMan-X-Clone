using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bat_States :State
{
    protected Vector2 playerPos;
    protected GameObject player;
    protected Vector2 currPos;
    protected Bat_Enemy self;
    
   public Bat_States(Bat_Enemy self)
    {
        this.self = self;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Enter()
    {
        playerPos = player.transform.position;
        
        base.Enter();
    }
    public override void Update()
    {
        playerPos = player.transform.position;
        currPos = self.gameObject.transform.position;
    }

}
