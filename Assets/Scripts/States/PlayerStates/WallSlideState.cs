using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallSlideState : PlayerState
{
    private float slideSpeed=3.25f;
    private bool wallDirection;
    private bool dashing;
    private float wallJumpForce=5f;
    
    public WallSlideState(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script) : base(player, anim, inputValueX, facingRight, script)
    {

    }
   

    public override void Enter()
    {
        wallDirection = facingRight;
        script.flippingCoroutine = script.StartCoroutine(flipRoutine());
        anim.SetTrigger("WallSlideTrigger");
        anim.SetBool("IsWallSliding",true);
        base.Enter();

    }
    public override void Exit()
    {
        
        anim.SetBool("IsWallSliding", false);
        if(script.flippingCoroutine!=null)
            script.StopCoroutine(script.flippingCoroutine);
        if (flippingRoutineRunning)
            flippingRoutineRunning = false;
        else
            Flip();
        base.Exit();
    }

    public override void Update()
    {
        Slide();
        if(script.IsGrounded())
            SwitchToGroundedState();
        if (SlideCheck(wallDirection))
            SwitchToAirPhase(dashing);

    }

    

    public override void OnJump(InputAction.CallbackContext context,bool wasDashing)
    {
        if (context.started)
        {
            float yValue = 1100f;
            float xValue;
            if (dashing)
                xValue = 750f;
            else
                xValue = 500f;
            if(wallDirection)
            {
                rigidbody2D.AddForce(new Vector2(-xValue, yValue), ForceMode2D.Impulse);
                script.StartCoroutine(ResetAirMomentum());
                SwitchToAirPhase(dashing);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(xValue,yValue), ForceMode2D.Impulse);
                script.StartCoroutine(ResetAirMomentum());
                SwitchToAirPhase(dashing);
            }
             
        }


    }
    public override void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
            dashing = true;
        if(context.canceled)
            dashing = false;

    }

    private void Slide()
    {
        rigidbody2D.velocity = new Vector2(0, -slideSpeed);

        deltaX = inputValueX * stats.moveSpeed * Time.deltaTime;

        if (Mathf.Abs(DeltaX) > 0)
        {
          if(wallDirection && DeltaX<0)
                SwitchToAirPhase(dashing);
          else if(!wallDirection && DeltaX>0)
                SwitchToAirPhase(dashing);
        }

    }

}
