using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//need to create a system so that my jump has a max set height but can be canceled by letting go of the button
public class AirState :PlayerState
{
    private bool wasDashing;
    private Rigidbody2D rigidbody2D;
    public AirState(GameObject player, Animator anim,float inputValueX, bool wasDashing,bool facingRight,PlayerCharacter script,Rigidbody2D rigidbody2D)
        : base(player, anim, inputValueX,facingRight,script)
    {
        this.rigidbody2D = rigidbody2D;
        this.wasDashing = wasDashing;
    }
    public override void Enter()
    {
        if(rigidbody2D.velocity.y>0)
        {
            anim.SetBool("IsJumping", true);
        }
        else if(rigidbody2D.velocity.y<=0)
        {
            anim.SetBool("IsFalling", true);
        }
        base.Enter();
    }

    public override void Exit()
    {
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
        anim.ResetTrigger("HitGround");

        base.Exit();
    }

    public override void OnJump(InputAction.CallbackContext context)
    {
        if(context.canceled&& player.GetComponent<Rigidbody2D>().velocity.y>0)
        {
            rigidbody2D.velocity = new Vector2(deltaX, 0);
        }
    }

    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
    }

    public override void Update()
    {
        Move();
        CheckIfFalling();
        if(script!=null&&script.IsGrounded())
        {
            SwitchToGroundedState();
        }
       
    }

   
    public void Move()
    {
        if(!wasDashing)
        deltaX = inputValueX * stats.moveSpeed * Time.deltaTime;
        else
            deltaX = inputValueX * stats.moveSpeed *stats.dashSpeed* Time.deltaTime;
        if (Mathf.Abs(deltaX) > 0)
        {
            player.transform.position = new Vector3(player.transform.position.x + deltaX, player.transform.position.y);
            if (deltaX < 0 && facingRight)
                Flip();
            else if (deltaX > 0 && !facingRight)
                Flip();
        }
       
    }
    private void SwitchToGroundedState()
    {
        if (rigidbody2D.velocity.y <= 0)
        {
            
            nextState = new GroundedState(player, anim, deltaX, facingRight, script);
            this.phase = Phase.EXIT;
        }
    }
    private void CheckIfFalling()
    {
        if(rigidbody2D.velocity.y<=0)
        {
            anim.SetBool("IsFalling", true);
        }
    }
    public override void OnShoot(InputAction.CallbackContext context)
    {
        base.OnShoot(context);
    }
}
