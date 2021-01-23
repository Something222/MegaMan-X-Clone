using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyEnemy : LivingEntities
{
    [SerializeField] private new BunnyStates currState;
    [SerializeField] private GameObject bullet;

    
    //GroundCheckFields
    [SerializeField] private float heightCheck;
    [SerializeField] private LayerMask layerMask;

    //cachedfields
    private Animator anim;
    private PlayerCharacter player;

  

    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        anim = GetComponent<Animator>();
        currState = new BunnyIdle(this, anim,player);
        
    }

    // Update is called once per frame
    void Update()
    {
        currState=(BunnyStates)currState.Process();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCharacter>().TakeDamage(Damage);
        }
    }

    public virtual bool IsGrounded(CapsuleCollider2D capsuleCollider)
    {

        RaycastHit2D rayCastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, heightCheck, layerMask);

        if (rayCastHit.collider != null)
        {
            return true;
        }
        else return false;

    }

}
