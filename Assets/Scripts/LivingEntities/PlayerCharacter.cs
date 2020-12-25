﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : LivingEntities
{
    [SerializeField] private LayerMask platformLayerMask;
    private float distToGround;
    private BoxCollider2D capsule;
    [SerializeField] private float heightCheck;
    new PlayerState currstate;

    //Coroutines
    public Coroutine shootingCoroutines;
    public Coroutine StopDashing;
    public float DistToGround { get => distToGround; }
    public float BulletSpeed { get => bulletSpeed; }
    public float BulletXOffset { get => bulletXOffset; set => bulletXOffset = value; }


    //BulletStuff
    [SerializeField]private float bulletSpeed=10f;
    [SerializeField] private float bulletXOffset = 1f;


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health>0)
        {
            currstate.SwitchToDamagedState();
            
        }
       // else
            //die();
    }
    public void ExitPhase()
    {
        currstate.phase = State.Phase.EXIT;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currstate.OnMove(context);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        currstate.OnJump(context);
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started)
        currstate.OnShoot(context);
    }
    public void OnDash(InputAction.CallbackContext context)
    {
            currstate.OnDash(context);
    }
    public virtual bool IsGrounded()
    {
      
        RaycastHit2D rayCastHit = Physics2D.BoxCast(capsule.bounds.center, capsule.bounds.size,0,Vector2.down,.5f,platformLayerMask);

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
    // Start is called before the first frame update
    protected override void Start()
    {
        capsule = GetComponent<BoxCollider2D>();
        distToGround = capsule.bounds.extents.y;
        
        base.Start();
        currstate = new GroundedState(gameObject, anim,0,true,this);
        
    }

    // Update is called once per frame
    void Update()
    {
       currstate=(PlayerState)currstate.Process();
    }
   
}
