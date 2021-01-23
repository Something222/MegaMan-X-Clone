using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : PlayerState
{
    bool endRunning;
    private float dashTimer = 1f;
    private float extraBulletOffset=.5f;
    private static DashState instance = null;

    public static DashState GetInstance(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script, bool endrunning)
    {
        if (instance == null)
            instance = new DashState(player, anim, inputValueX, facingRight, script, endrunning);
        else
        {
            instance.phase = Phase.ENTER;
            instance.inputValueX = inputValueX;
            instance.facingRight = facingRight;
            instance.endRunning = endrunning;
        }
        return instance;
    }
    public IEnumerator DashStop()
    {
        yield return new WaitForSeconds(dashTimer);
        SwitchToGroundedState();
    }

    private void SwitchToGroundedState()
    {
        if (!endRunning)
            inputValueX = 0;
        nextState = GroundedState.GetInstance(player, anim, inputValueX, facingRight, script);
        phase = Phase.EXIT;

    }

    public DashState(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script,bool endrunning) : base(player, anim, inputValueX, facingRight, script)
    {
        this.endRunning = endrunning;
    }
    public override void Enter()
    {
        script.StartCoroutine(script.DashStarted());
       // script.BoxCollider.enabled = false;
        script.CapsuleCollider.direction = CapsuleDirection2D.Horizontal;
        script.CapsuleCollider.size = new Vector2(script.DashColliderSizeX, script.DashColliderSizeY);


        if (facingRight)
            inputValueX = 1;
        else
            inputValueX = -1;
        anim.SetBool("IsDashing", true);
        script.StopDashing = script.StartCoroutine(DashStop());
        script.BulletXOffset +=extraBulletOffset;
        base.Enter();
    }
    public override void Update()
    {
        Move();
        if (script != null && !script.IsGrounded() &&!script.dashTransitioning)
            SwitchToAirPhase(true);
    }
    public override void Exit()
    {
      
        anim.SetBool("IsDashing", false);
        if (script.StopDashing != null)
            script.StopCoroutine(script.StopDashing);

        script.BulletXOffset -= extraBulletOffset;
        base.Exit();
       
    }
    public override void OnJump(InputAction.CallbackContext context,bool wasDashing)
    {
        base.OnJump(context,true);
    }
    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);

        if (context.started)
            endRunning = true;
        else if (context.canceled)
        {
            endRunning = false;
            SwitchToGroundedState();
        }
    }
    public override void OnShoot(InputAction.CallbackContext context)
    {
        base.OnShoot(context);
    }
    public override void OnDash(InputAction.CallbackContext context)
    {
        if(context.canceled)
            SwitchToGroundedState();
    }
    protected override void Flip()
    {
        
        base.Flip();
        SwitchToGroundedState();
    }
    public void Move()
    {
        deltaX = inputValueX * stats.moveSpeed*stats.dashSpeed * Time.deltaTime;
        if (Mathf.Abs(DeltaX) > 0)
        {
            if(MoveCheckDash())
            player.transform.position = new Vector3(player.transform.position.x + DeltaX, player.transform.position.y);
            if (DeltaX < 0 && facingRight)
                Flip();
            else if (DeltaX > 0 && !facingRight)
                Flip();
        }
       
    }

}
