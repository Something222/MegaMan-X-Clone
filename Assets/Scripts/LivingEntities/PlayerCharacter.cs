﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : LivingEntities
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] new PlayerState currState;

    //Cached Variables
    private CapsuleCollider2D capsule;
   [SerializeField] private ParticleSystem chargeParticleFX;

    //Movement Variables
    Vector3 groundChecker;
    public bool cantMove;
    public bool cantTransition;
    [SerializeField] private float heightCheck;
    private float distToGround;
   

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
    public CapsuleCollider2D Capsule { get => capsule;}
    public float JumpingBulletYOffset { get => jumpingBulletYOffset; }
    public ParticleSystem ChargeParticleFX { get => chargeParticleFX; set => chargeParticleFX = value; }

    public void TakeDamage(int damage)
    {
        if (!invicibility)
        {
            invicibility = true;
            StartCoroutine(TurnOffInvicibility());
            health -= damage;

            if (health > 0)
                currState.SwitchToDamagedState();

        }
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
      
        RaycastHit2D rayCastHit = Physics2D.BoxCast(Capsule.bounds.center, Capsule.bounds.size,0,Vector2.down,.25f,PlatformLayerMask);

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
            Debug.Log(chargeLevel);
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
 
    // Start is called before the first frame update
    protected override void Start()
    {
        // Capsule = GetComponent<BoxCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        distToGround = Capsule.bounds.extents.y;
        groundChecker=new Vector3(Capsule.bounds.center.x-.2f, Capsule.bounds.center.y, Capsule.bounds.center.z);
        base.Start();
        chargeLevel = 0;
        currState = new GroundedState(gameObject, anim,0,true,this);
        chargeParticleFX = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
       currState=(PlayerState)currState.Process();
        Debug.Log(currState);
    }

    public override void Respawn()
    {
        throw new System.NotImplementedException();
    }



}
