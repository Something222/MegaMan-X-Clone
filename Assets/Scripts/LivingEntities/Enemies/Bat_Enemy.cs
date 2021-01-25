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
    
    [SerializeField] public int retreatRange = 10;
    [SerializeField] public float attackMoveSpeed = 3.5f;
    [SerializeField] public float retreatMoveSpeed = 4.5f;

    private Vector2 awakeColliderSize;
    private Vector2 asleepColliderSize;

    private GameObject player;

    public Vector2 AwakeColliderSize => awakeColliderSize;
    public Vector2 AsleepColliderSize => asleepColliderSize;
    //Components
    public BoxCollider2D collider;
    private Rigidbody2D body;
    

    //Stats
    
    //Player Reference this will go into the enemy state
    private Transform playerPos;

 
    public Animator Anim { get => anim; }
    private void Awake()
    {
        MaxHealth = health;
    }

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
        currState = Bat_Asleep.GetInstance(this, player);
    }

    public override void Respawn()
    {
        base.Respawn();
        moveSpeed = 3.5f;
        if(player==null)
        player = FindObjectOfType<PlayerCharacter>().gameObject;


        currState = new Bat_Asleep(this, player);
<<<<<<< HEAD
    

        currState = Bat_Asleep.GetInstance(this, player);
        Debug.Log(currState);

=======
        Debug.Log(currState);
>>>>>>> parent of bba94b1... ok
    }

    // Update is called once per frame
    void Update()
    {
        currState=(Bat_States)currState.Process();
     
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currState = Bat_Retreat.GetInstance(this, player);
            collision.GetComponent<PlayerCharacter>().TakeDamage(Damage);
        }

    }

  

}
