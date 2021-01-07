using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntities : MonoBehaviour
{
    [SerializeField] public float health=100;
    [SerializeField]private float maxHealth;
    [SerializeField]public float moveSpeed=1f;
    [SerializeField] public State currState;
    [SerializeField] public float jumpHeight=5;
    public bool invicibility = false;
     public float dashSpeed = 2;
    [SerializeField]public Animator anim;
    [SerializeField] private float iFrames = .55f;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

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
}
