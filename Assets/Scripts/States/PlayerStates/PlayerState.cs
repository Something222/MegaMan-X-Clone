﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState : State
{
    protected bool facingRight = true;
    protected float inputValueX;
    protected float deltaX;
    protected LivingEntities stats;
    protected Animator anim;
    protected PlayerCharacter script;
    protected GameObject player;
    protected float boxCastBuffer = .29f;
    protected Rigidbody2D rigidbody2D;
    protected bool flippingRoutineRunning;

    public PlayerState(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script)
    {
            this.inputValueX = inputValueX;
            this.player = player;
            this.anim = anim;
            this.phase = Phase.ENTER;
            this.facingRight = facingRight;
            this.script = script;
            stats = player.GetComponent<LivingEntities>();  
    }
    public bool MoveCheck()
    {
        RaycastHit2D hit;
        Vector2 direction;
        if (facingRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;
        if (script.IsGrounded())
            hit = Physics2D.Raycast(script.Capsule.bounds.center, direction, 1f,script.PlatformLayerMask);
        else
            hit = Physics2D.BoxCast(script.Capsule.bounds.center, script.Capsule.bounds.size, 0, direction, boxCastBuffer, script.PlatformLayerMask);

        if(hit.collider!=null)
            return false;

        return true;
    }

    protected bool SlideCheck(bool wallDirection)
    {
        RaycastHit2D hit;
        Vector2 direction;
        if (wallDirection)
            direction = Vector2.right;
        else
            direction = Vector2.left;
        hit = Physics2D.BoxCast(script.Capsule.bounds.center, script.Capsule.bounds.size, 0, direction, boxCastBuffer, script.PlatformLayerMask);

        if (hit.collider != null)
            return false;
        return true;
    }
    public virtual void OnMove(InputAction.CallbackContext context)
    {
        inputValueX = context.ReadValue<Vector2>().x;
        RoundInputValueX(); 
    }
    public virtual void OnDash(InputAction.CallbackContext context)
    {

    }
    private void RoundInputValueX()
    {
        if (inputValueX < 0)
            inputValueX = -1;
        else if (inputValueX > 0)
            inputValueX = 1;
        
    }

    public virtual void OnJump(InputAction.CallbackContext context,bool wasDashing)
    {
        if (context.started)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(deltaX, stats.jumpHeight);
            SwitchToAirPhase(wasDashing);
        }
    }
    public virtual void OnShoot(InputAction.CallbackContext context)
    {

        if (script.chargeLevel == 0)
        {
            if (ShotPool.Instance.Objects.Count != 0)
            {
                SetShootingAnims();
                GameObject obj = ShotPool.Instance.Get().gameObject;
                SetBulletPos(obj);
            }
        }
        else if(script.chargeLevel==1)
        {
            if(Level1ShotPool.Instance.Objects.Count!=0)
            {
                SetShootingAnims();
                GameObject obj = Level1ShotPool.Instance.Get().gameObject;
                SetBulletPos(obj);

            }

        }
    }

    private void SetBulletPos(GameObject obj)
    {
        if (obj != null)
        {
            Vector3 newpos = player.transform.position;
            float xScale, xOffSet, yOffSet, bulletSpeed;
            if (facingRight)
            {
                xScale = 1;
                xOffSet = newpos.x + script.BulletXOffset;
                if (script.IsGrounded())
                    yOffSet = newpos.y;
                else
                    yOffSet = newpos.y + script.JumpingBulletYOffset;
                bulletSpeed = script.BulletSpeed;

            }
            else
            {
                xScale = -1;
                xOffSet = newpos.x - script.BulletXOffset;
                if (script.IsGrounded())
                    yOffSet = newpos.y;
                else
                    yOffSet = newpos.y + script.JumpingBulletYOffset;
                bulletSpeed = -script.BulletSpeed;
            }
            obj.transform.localScale = new Vector3(xScale, 1, 1);
            obj.transform.position = new Vector3(xOffSet, yOffSet, newpos.z);
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletSpeed, 0, 0);
        }
    }

    private void SetShootingAnims()
    {
        anim.SetBool("IsShooting", true);
        anim.SetFloat("ShootOffset", 1);
        script.StartResetShooting();
    }

    public void SwitchToDamagedState()
    {
        nextState = new DamagedState(player, anim, inputValueX, facingRight, script);
        phase = Phase.EXIT;
    }
    protected void SwitchToDashState(bool wasRunning)
    {
        //anim.SetBool("IsRunning", false);
        nextState = new DashState(player, anim, inputValueX, facingRight, script, wasRunning);
        phase = Phase.EXIT;
    }
    public IEnumerator flipRoutine()
    {
        flippingRoutineRunning = true;
        yield return new WaitForSeconds(.085f);
        facingRight = !facingRight;
        flippingRoutineRunning=false;
    }
    public IEnumerator ResetAirMomentum()
    {
        script.cantMove =true;
        yield return new WaitForSeconds(.35f);
        script.cantMove = false;
        rigidbody2D.velocity = new Vector2(deltaX, rigidbody2D.velocity.y);
    }

    public IEnumerator AirToSlideBuffer()
    {
        script.cantTransition = true;
        yield return new WaitForSeconds(.1f);
        script.cantTransition = false;
    }

    protected void SwitchToAirPhase(bool dash)
    {
        nextState = new AirState(player, anim, inputValueX, dash, facingRight, script);
        phase = Phase.EXIT;
    }
    protected void SwitchToWallSlideState()
    {
        nextState = new WallSlideState(player, anim, inputValueX, facingRight, script);
        phase = Phase.EXIT;

    }
    protected void SwitchToGroundedState()
    {
       
            nextState = new GroundedState(player, anim, deltaX, facingRight, script);
            this.phase = Phase.EXIT;
       
    }
    
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempscale = player.transform.localScale;
        tempscale.x *= -1;
        player.transform.localScale = tempscale;
    }
    protected virtual void Flip(float DeltaX)
    {
        if (DeltaX > 1)
        {
            facingRight = true;
        }
        else
            facingRight = false;
        Vector3 tempscale = player.transform.localScale;
        tempscale.x *= -1;
        player.transform.localScale = tempscale;
    }
    protected void RightCheck()
    {
        if (player.transform.localScale.x > 0)
            facingRight = true;
        else
            facingRight = false;
    }
    public override void Enter()
    {
        RoundInputValueX();
        rigidbody2D = player.GetComponent<Rigidbody2D>();
        RightCheck();
        base.Enter();
    }

    

}
