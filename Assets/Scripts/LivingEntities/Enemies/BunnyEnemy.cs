﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyEnemy : LivingEntities
{
    [SerializeField] private new BunnyStates currState;
    [SerializeField] private GameObject bullet;

    
    //GroundCheckFields
    [SerializeField] private float heightCheck;
   

    //cachedfields
    private Animator anim;
    private PlayerCharacter player;

    public float HeightCheck { get => heightCheck; }

    void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerCharacter>();
        anim = GetComponent<Animator>();
        currState = new BunnyIdle(this, anim,player);
        
    }

    // Update is called once per frame
    void Update()
    {
        currState=(BunnyStates)currState.Process();
        Debug.Log(currState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCharacter>().TakeDamage(Damage);
        }
    }

  

}
