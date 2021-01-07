using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Bat_Enemy : LivingEntities
{
    //State
   [SerializeField] private new Bat_States curState;

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

    public Vector2 AwakeColliderSize => awakeColliderSize;
    public Vector2 AsleepColliderSize => asleepColliderSize;
    //Components
    public BoxCollider2D collider;
    private Rigidbody2D body;
    

    //Stats
    
    //Player Reference this will go into the enemy state
    private Transform playerPos;

 
    public Animator Anim { get => anim; }

    protected override void Start()
    {
        base.Start();
        moveSpeed = 3.5f;
        asleepColliderSize = new Vector2(asleepXColliderSize, asleepYColliderSize);
        awakeColliderSize = new Vector2(awakeXColliderSize, awakeYColliderSize);
        collider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        collider.size = asleepColliderSize;
        
       
        curState = new Bat_Asleep(this);
    }

    public override void Respawn()
    {
        base.Respawn();
        curState = new Bat_Asleep(this);

    }

    // Update is called once per frame
    void Update()
    {
        curState=(Bat_States)curState.Process();
       // Debug.Log(curState);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            curState = new Bat_Retreat(this);
            collision.GetComponent<PlayerCharacter>().TakeDamage(damage);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Spawner")
        {
            gameObject.SetActive(false);
        }
    }

}
