using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMoving : BunnyStates
{
    private bool canSwitch = false;//We will check if the bunny hits the ground to see if we transition but when he starts hes grounded
    private float switchTimer = .5f;
    private CapsuleCollider2D capsuleCollider;

    public BunnyMoving(BunnyEnemy self, Animator anim, PlayerCharacter player) : base(self, anim, player) { }

    private IEnumerator Switching()
    {
        yield return new WaitForSeconds(switchTimer);
        canSwitch = true;
    }
    private void Jump()
    {
        if(self.gameObject.transform.localScale.x==1)
        {
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(-self.moveSpeed, self.jumpHeight));
        }
        else if (self.gameObject.transform.localScale.x == -1)
        {
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(self.moveSpeed, self.jumpHeight));
        }
    }

    public override void Enter()
    {
        anim.SetTrigger("Jump");
        capsuleCollider = self.GetComponent<CapsuleCollider2D>();
        self.StartCoroutine(Switching());
        Jump();
        base.Enter();
    }

    public override void Update()
    {
        if (canSwitch && self.IsGrounded(capsuleCollider))
        {
            nextState = new BunnyIdle(self, anim, player);
            anim.SetBool("Idle", true);
            anim.SetTrigger("Landed");
            phase = Phase.EXIT;
            
        }
    }



}
