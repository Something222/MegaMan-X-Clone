using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyStates : State
{
    protected Rigidbody2D body;
    protected PlayerCharacter player;
    protected GameObject bullet;
    protected BunnyEnemy self;
    protected Animator anim;

    protected BunnyStates(BunnyEnemy self, Animator anim,PlayerCharacter player)
    {
        this.self = self;
        this.anim = anim;
        this.player = player;
    }
    
}
