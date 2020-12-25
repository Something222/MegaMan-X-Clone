using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntities : MonoBehaviour
{
    [SerializeField] public float health=100;
    [SerializeField]public float moveSpeed=1f;
    [SerializeField] public State currstate;
    [SerializeField] public float jumpHeight=5;
     public float dashSpeed = 2;
    [SerializeField]public Animator anim;
    
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Make a death Function 
    public virtual void Die()
    {

        Destroy(gameObject);
        //anim.setTrigger(Die) //this will call the destroy object
        //Switch to deathState
    }

    public virtual void DeactivateObject()
    {
        this.gameObject.SetActive(false);
    }
    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

}
