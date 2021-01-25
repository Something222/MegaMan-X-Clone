using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntities : MonoBehaviour
{

    //Stats
    [SerializeField] public float health=100;
    [SerializeField]private float maxHealth;
    [SerializeField]public float moveSpeed=1f;
    [SerializeField] public State currState;
    [SerializeField] public float jumpHeight=5;
    [SerializeField] private float iFrames = .55f;
    [SerializeField] private int damage = 10;
    public bool invicibility = false;
     public float dashSpeed = 2;

    [SerializeField] private float heightCheck;
    [SerializeField] private LayerMask layerMask;

    //cached Fields
    [SerializeField]public Animator anim;
   
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Damage { get => damage;  }
    public float HeightCheck { get => heightCheck; }
    public LayerMask PlatformLayerMask { get => layerMask;}

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
    }

    //Make a death Function 
    public virtual void Die()
    {

        //Destroy(gameObject);
        this.gameObject.SetActive(false);
        //anim.setTrigger(Die) //this will call the destroy object
        //Switch to deathState
    }

    public virtual void DeactivateObject()
    {
        this.gameObject.SetActive(false);
    }
  
    public IEnumerator TurnOffInvicibility()
    {
        yield return new WaitForSeconds(iFrames);
        invicibility = false;
    }
    public virtual void Respawn()
    {
        invicibility = false;
        health = maxHealth;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Spawner")
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        if (!invicibility)
        {
            invicibility = true;
            StartCoroutine(TurnOffInvicibility());
            health -= damage;

        }
    }

}
