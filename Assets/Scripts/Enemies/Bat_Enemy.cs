using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Bat_Enemy : LivingEntities
{
    //State
   [SerializeField] private new Bat_States currState;

    //Collider Size Differences
    [SerializeField] private float awakeXColliderSize;
    [SerializeField] private float awakeYColliderSize;
    [SerializeField] private float asleepXColliderSize;
    [SerializeField] private float asleepYColliderSize;

    //Stats
    [SerializeField] private int damage = 10;
    [SerializeField] public int retreatRange = 10;
    [SerializeField] public float attackMoveSpeed = 3.5f;
    [SerializeField] public float retreatMoveSpeed = 4.5f;

    private Vector2 awakeColliderSize;
    private Vector2 asleepColliderSize;
    //Components
    private BoxCollider2D collider;
    private Rigidbody2D body;
    

    //Stats
    
    //Player Reference this will go into the enemy state
    private Transform playerPos;

 
    public Animator Anim { get => anim; }

    void Start()
    {
        base.Start();
        moveSpeed = 3.5f;
        asleepColliderSize = new Vector2(asleepXColliderSize, asleepYColliderSize);
        awakeColliderSize = new Vector2(awakeXColliderSize, awakeYColliderSize);
        collider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        collider.size = asleepColliderSize;
        currState = new Bat_Asleep(this);
    }

    // Update is called once per frame
    void Update()
    {
        currState=(Bat_States)currState.Process();
     
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            currState = new Bat_Retreat(this);
            collision.GetComponent<PlayerCharacter>().TakeDamage(damage);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Spawner")
        {
            Destroy(gameObject);
        }
    }

}
