using System.Collections;
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
    public virtual void OnMove(InputAction.CallbackContext context)
    {
        inputValueX = context.ReadValue<Vector2>().x;
        
        RoundInputValueX();
        Debug.Log(inputValueX);
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

    public abstract void OnJump(InputAction.CallbackContext context);
    public virtual void OnShoot(InputAction.CallbackContext context)
    {
        anim.SetBool("IsShooting", true);
        anim.SetFloat("ShootOffset", 1);
        script.StartResetShooting();
        GameObject obj = Pool.instance.Get("Bullet");
        if (obj != null)
        {
            Vector3 newpos = player.transform.position;
            obj.SetActive(true);
            if (facingRight)
            {
                obj.transform.position = new Vector3(newpos.x + script.BulletXOffset, newpos.y, newpos.z);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector3(script.BulletSpeed, 0, 0);
            }
            else
            {
                obj.transform.position = new Vector3(newpos.x - script.BulletXOffset, newpos.y, newpos.z);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector3(-script.BulletSpeed, 0, 0);
            }

        }

    }

    public void SwitchToDamagedState()
    {
        nextState = new DamagedState(player, anim, inputValueX, facingRight, script, player.GetComponent<Rigidbody2D>());
        phase = Phase.EXIT;
    }
    protected void SwitchToAirPhase(bool dash)
    {
        nextState = new AirState(player, anim, inputValueX, dash, facingRight, script, player.GetComponent<Rigidbody2D>());
        phase = Phase.EXIT;
    }
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempscale = player.transform.localScale;
        tempscale.x *= -1;
        player.transform.localScale = tempscale;
    }
   
    public override void Enter()
    {
        RoundInputValueX();
        base.Enter();
    }

    

}
