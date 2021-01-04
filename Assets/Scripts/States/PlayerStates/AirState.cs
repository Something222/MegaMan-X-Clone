﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//need to create a system so that my jump has a max set height but can be canceled by letting go of the button
public class AirState :PlayerState
{
    private bool wasDashing;
   
    public AirState(GameObject player, Animator anim,float inputValueX, bool wasDashing,bool facingRight,PlayerCharacter script)
        : base(player, anim, inputValueX,facingRight,script)
    {
        this.wasDashing = wasDashing;
    }
    public override void Enter()
    {
        base.Enter();
        if (rigidbody2D.velocity.y>0)
            anim.SetBool("IsJumping", true);
        else if(rigidbody2D.velocity.y<=0)
            anim.SetBool("IsFalling", true);
      
    }

    public override void Exit()
    {
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
        anim.ResetTrigger("HitGround");

        base.Exit();
    }

    public override void OnJump(InputAction.CallbackContext context,bool wasdashing)
    {
        if (rigidbody2D != null)
        {
            if (context.canceled && rigidbody2D.velocity.y > 0 && script.cantMove)
            {
                rigidbody2D.velocity = new Vector2(deltaX, rigidbody2D.velocity.y);
                script.cantMove = false;
            }
            else if (context.canceled && rigidbody2D.velocity.y > 0 && !script.cantMove)
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
        if(script!=null&&script.IsGrounded() && rigidbody2D.velocity.y <= 0)
            SwitchToGroundedState();
    }

   
    public void Move()
    {
      
        if(!wasDashing)
             deltaX = inputValueX * stats.moveSpeed * Time.deltaTime;
        else
            deltaX = inputValueX * stats.moveSpeed *stats.dashSpeed* Time.deltaTime;
        if (Mathf.Abs(deltaX) > 0 && !script.cantMove)
        {
                player.transform.position = new Vector3(player.transform.position.x + deltaX, player.transform.position.y);
                if (!MoveCheck())
                    SwitchToWallSlideState(); 
                if (deltaX < 0 &&player.transform.localScale.x>0)
                    Flip();
                else if (deltaX > 0 && player.transform.localScale.x < 0)
                    Flip();
        }
       
    }
 
    private void CheckIfFalling()
    {
        if(rigidbody2D.velocity.y<=0)
            anim.SetBool("IsFalling", true);
    }
    public override void OnShoot(InputAction.CallbackContext context)
    {
        base.OnShoot(context);
    }
}
