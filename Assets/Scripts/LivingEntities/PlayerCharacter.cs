﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : LivingEntities
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] new public PlayerState currState;

    //Cached Variables
   [SerializeField]  private CapsuleCollider2D capsuleCollider;
    [SerializeField]private CapsuleCollider2D FeetCollider;
   [SerializeField] private ParticleSystem chargeParticleFX;


    //Movement Variables
    Vector3 groundChecker;
    public bool cantMove;
    public bool cantTransition;
  
    private float distToGround;

    //ColliderSizes capsule
    [SerializeField] private float groundedCapsuleColliderSizeX;
    [SerializeField] private float groundedCapsuleColliderSizeY;
    [SerializeField] private float airCapsuleColliderSizeX;
    [SerializeField] private float airCapsuleColliderSizeY;


    //ColliderSizes Box
    [SerializeField] private float groundedBoxColliderSizeX;
    [SerializeField] private float airBoxColliderSizeX;

    //ColliderSizes Dash
    [SerializeField] private float dashColliderSizeX;
    [SerializeField] private float dashColliderSizeY;

    //Dash Check
    public bool dashTransitioning;
    [SerializeField] private float dashTransitionTimer;

    //Coroutines
    public Coroutine shootingCoroutines;
    public Coroutine StopDashing;
    public Coroutine flippingCoroutine;
    public Coroutine chargeShotCoroutine;


    //BulletStuff
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletXOffset = 1f;
    [SerializeField] private float jumpingBulletYOffset;
    public int chargeLevel;
    [SerializeField] private float chargeTime = 1f;

    //Properties
    public float DistToGround { get => distToGround; }
    public float BulletSpeed { get => bulletSpeed; }
    public float BulletXOffset { get => bulletXOffset; set => bulletXOffset = value; }
    public LayerMask PlatformLayerMask { get => platformLayerMask; }
    public CapsuleCollider2D CapsuleCollider { get => capsuleCollider;}
    public float JumpingBulletYOffset { get => jumpingBulletYOffset; }
    public ParticleSystem ChargeParticleFX { get => chargeParticleFX; set => chargeParticleFX = value; }
    public float GroundedColliderSizeX { get => groundedCapsuleColliderSizeX; }
    public float GroundedColliderSizeY { get => groundedCapsuleColliderSizeY;  }
    public float AirColliderSizeX { get => airCapsuleColliderSizeX; }
    public float AirColliderSizeY { get => airCapsuleColliderSizeY;  }
    public float GroundedBoxColliderSizeX { get => groundedBoxColliderSizeX;}
    public float AirBoxColliderSizeX { get => airBoxColliderSizeX; }
    public CapsuleCollider2D BoxCollider { get => FeetCollider;}
    public float DashColliderSizeX { get => dashColliderSizeX; }
    public float DashColliderSizeY { get => dashColliderSizeY;  }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
     
            if (health > 0)
                currState.SwitchToDamagedState();
   
       // else
            //die();
    }
    public void ExitPhase()
    {
        currState.phase = State.Phase.EXIT;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currState.OnMove(context);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        currState.OnJump(context,false);
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            chargeLevel = 0;
            chargeShotCoroutine = StartCoroutine(ChargeShot());
        }
        if (context.canceled)
        {
            currState.OnShoot(context);
            if (chargeShotCoroutine != null)
                StopCoroutine(chargeShotCoroutine);
          
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
            currState.OnDash(context);
    }
    public virtual bool IsGrounded()
    {
      
        RaycastHit2D rayCastHit = Physics2D.BoxCast(CapsuleCollider.bounds.center, CapsuleCollider.bounds.size,0,Vector2.down,HeightCheck,PlatformLayerMask);

        if (rayCastHit.collider != null)
        {
            return true; 
        }
        else return false;
       
    }
    public void StartResetShooting()
    {
        if(shootingCoroutines!=null)
        StopCoroutine(shootingCoroutines);
      shootingCoroutines=StartCoroutine(ResetShooting());
    }
    public IEnumerator ResetShooting()
    {
        yield return new WaitForSeconds(.3f);
        anim.SetBool("IsShooting", false);
        anim.SetFloat("ShootOffset", 0);
    }

    public IEnumerator ChargeShot()
    {
        while(true)
        {
           
            yield return new WaitForSeconds(chargeTime);
            if (chargeLevel < 2)
                chargeLevel++;
            if(chargeLevel==1)
            {
                var main = ChargeParticleFX.main;
                main.startColor = Color.green;
                ChargeParticleFX.Play();
            }
            else if (chargeLevel==2)
            {
                var main = ChargeParticleFX.main;
                main.startColor = Color.blue;
            }
        }
    }

    public IEnumerator DashStarted()
    {
        dashTransitioning = true;
        yield return new WaitForSeconds(dashTransitionTimer);
        dashTransitioning = false;
    }
 

    protected override void Start()
    {
      
        distToGround = CapsuleCollider.bounds.extents.y;
        groundChecker=new Vector3(CapsuleCollider.bounds.center.x-.2f, CapsuleCollider.bounds.center.y, CapsuleCollider.bounds.center.z);
        base.Start();
        chargeLevel = 0;
        // currState = new GroundedState(gameObject, anim,0,true,this);
        currState = GroundedState.GetInstance(gameObject, anim, 0, true, this);
        chargeParticleFX = GetComponentInChildren<ParticleSystem>();
      
    }


    void Update()
    {
       currState=(PlayerState)currState.Process();
       
    }

    public override void Respawn()
    {
        throw new System.NotImplementedException();
    }



}
