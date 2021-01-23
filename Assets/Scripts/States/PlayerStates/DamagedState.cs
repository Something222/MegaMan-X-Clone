using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamagedState : PlayerState
{

    private static DamagedState instance;
    private float recoilSpeed=3f;

    public static DamagedState GetInstance(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script)
    {
        if (instance == null)
            instance = new DamagedState(player, anim, inputValueX, facingRight, script);
        else
        {
            instance.phase = Phase.ENTER;
            instance.facingRight = facingRight;
            instance.inputValueX = inputValueX;
        }
        return instance;
    }
    public DamagedState(GameObject player, Animator anim, float inputValueX, bool facingRight, PlayerCharacter script)
    : base(player, anim, inputValueX, facingRight, script)
    {
    }
   

    public override void Enter()
    {
        anim.SetTrigger("IsDamaged");
        script.chargeLevel = 0;
        if (script.ChargeParticleFX.isPlaying)
            script.ChargeParticleFX.Stop();
        base.Enter();
    }

    public override void Exit()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        if (script.IsGrounded())
        {
            //nextState = new GroundedState(player, anim, inputValueX, facingRight, script);
            nextState = GroundedState.GetInstance(player, anim, inputValueX, facingRight, script);
        }
        else
        {
            // nextState = new AirState(player, anim, inputValueX, false, facingRight, script);
            nextState = AirState.GetInstance(player, anim, inputValueX, false, facingRight, script);
        }
       

        base.Exit();
    }

  
    public override State Process()
    {
        return base.Process();
    }

    public override void Update()
    {
        Recoil();

    }

    private void Recoil()
    {
        rigidbody2D.velocity=facingRight? new Vector2(-recoilSpeed, 0) : new Vector2(recoilSpeed, 0);
    }


    //Override them make em blank
    public override void OnJump(InputAction.CallbackContext context,bool wasdashing)
    {

    }
    public override void OnShoot(InputAction.CallbackContext context)
    {

    }
  
    public override void OnDash(InputAction.CallbackContext context)
    {
  
    }
}
