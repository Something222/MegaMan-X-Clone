using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamagedState : PlayerState
{

    private Rigidbody2D rigidbody2D;
    private float recoilSpeed=3f;
    public DamagedState(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script,Rigidbody2D rigidbody)
    : base(player, anim, inputValueX, facingRight, script)
    {
        rigidbody2D = rigidbody;
    }
   

    public override void Enter()
    {
        anim.SetTrigger("IsDamaged");
      
        base.Enter();
    }

    public override void Exit()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        if (script.IsGrounded())
        {
            nextState = new GroundedState(player, anim, inputValueX, facingRight, script);
        }
        else
        {
            nextState = new AirState(player, anim, inputValueX, false, facingRight, script, player.GetComponent<Rigidbody2D>());
        }
       

        base.Exit();
    }

  
    public override State Process()
    {
        return base.Process();
    }

    public override void Update()
    {

        if (facingRight)
        {
            rigidbody2D.velocity = new Vector2(-recoilSpeed, 0);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(recoilSpeed, 0);
        }

    }


    //Override them make em blank
    public override void OnJump(InputAction.CallbackContext context)
    {

    }
    public override void OnShoot(InputAction.CallbackContext context)
    {

    }
  
    public override void OnDash(InputAction.CallbackContext context)
    {
  
    }
}
