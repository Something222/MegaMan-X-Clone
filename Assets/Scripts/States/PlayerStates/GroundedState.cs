using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedState : PlayerState
{
    //May want to a add IsGrounded to the animator state

    public GroundedState(GameObject player,Animator anim,float inputValueX,bool facingRight,PlayerCharacter script)
        :base(player,anim, inputValueX,facingRight,script){}


    public override void Enter()
    {
     
        anim.SetTrigger("HitGround");
        base.Enter();
       
    }

    public override void Exit()
    {
        anim.ResetTrigger("HitGround");
        anim.SetBool("IsRunning", false);
        base.Exit();
    }

    public override void Update()
    {
        Move();
        if(script!=null&&!script.IsGrounded())
        {
            SwitchToAirPhase(false);
        }

    }
    
    //old input system
    public void Move()
    {
        deltaX = inputValueX * stats.moveSpeed * Time.deltaTime;
       
        if (Mathf.Abs(deltaX) > 0)
        {
            player.transform.position = new Vector3(player.transform.position.x + deltaX, player.transform.position.y);
         
            anim.SetBool("IsRunning", true);

            if (deltaX < 0 && facingRight)
                Flip();
            else if (deltaX > 0 && !facingRight)
                Flip();

        }
        else
            anim.SetBool("IsRunning", false);
    }
   

    //so the new input system is basically event driven
    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
    }
    public override void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Mathf.Abs(deltaX) > 0)
                SwitchToDashState(true);
            else
                SwitchToDashState(false);
        }
    }
    public override void OnJump(InputAction.CallbackContext context)
    {
        //This will cause a state change to the AirState 
        //while also adding force in y to jump
        //we need to pass whether or not we were dashing for the airspeed
        if (context.started)
        {
            player.GetComponent<Rigidbody2D>().velocity=new Vector2(deltaX,stats.jumpHeight);
            SwitchToAirPhase(false);
        }

    }
    private void SwitchToDashState(bool wasRunning)
    {
        //anim.SetBool("IsRunning", false);
        nextState = new DashState(player, anim, inputValueX, facingRight, script,wasRunning);
        phase = Phase.EXIT;
    }
    
    public override void OnShoot(InputAction.CallbackContext context)
    {
        base.OnShoot(context);

    }
    
}
