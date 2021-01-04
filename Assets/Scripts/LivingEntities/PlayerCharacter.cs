using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : LivingEntities
{
    [SerializeField] private LayerMask platformLayerMask;
    private float distToGround;
    private BoxCollider2D capsule;
    [SerializeField] private float heightCheck;
   [SerializeField] new PlayerState currState;
    Vector3 groundChecker;
    public bool cantMove;

    //Coroutines
    public Coroutine shootingCoroutines;
    public Coroutine StopDashing;
    public Coroutine flippingCoroutine;


    public float DistToGround { get => distToGround; }
    public float BulletSpeed { get => bulletSpeed; }
    public float BulletXOffset { get => bulletXOffset; set => bulletXOffset = value; }
    public BoxCollider2D Capsule { get => capsule;}
    public LayerMask PlatformLayerMask { get => platformLayerMask; }


    //BulletStuff
    [SerializeField]private float bulletSpeed=10f;
    [SerializeField] private float bulletXOffset = 1f;


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
        if(context.started)
        currState.OnShoot(context);
    }
    public void OnDash(InputAction.CallbackContext context)
    {
            currState.OnDash(context);
    }
    public virtual bool IsGrounded()
    {
      
        RaycastHit2D rayCastHit = Physics2D.BoxCast(Capsule.bounds.center, Capsule.bounds.size,0,Vector2.down,.5f,PlatformLayerMask);

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
        distToGround = Capsule.bounds.extents.y;
        groundChecker=new Vector3(Capsule.bounds.center.x-.2f, Capsule.bounds.center.y, Capsule.bounds.center.z);


        base.Start();
        currState = new GroundedState(gameObject, anim,0,true,this);
        
    }

    // Update is called once per frame
    void Update()
    {
       currState=(PlayerState)currState.Process();
       
    }

    public override void Respawn()
    {
        throw new System.NotImplementedException();
    }
}
