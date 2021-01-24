using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFunctions 
{

    /// <summary>
    /// Depending on the scale aka the way the enemy is facing jump
    /// </summary>
    /// <param name="self">the enemy gameobject</param>
    /// <param name="xforce">force to jump on x</param>
    /// <param name="yforce">force to jump on y</param>
   public static void Jump(LivingEntities self,float xforce,float yforce)
    {
        if(self.gameObject.transform.localScale.x==1)
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xforce, yforce));
        else
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(xforce, yforce));
    }
    /// <summary>
    /// Will flip the sprite to look at character
    /// </summary>
    /// <param name="player"></param>
    /// <param name="self"></param>
    public static void TrackPlayer(PlayerCharacter player, GameObject self)
    {
        if (player.transform.position.x < self.transform.position.x)
            self.transform.localScale = new Vector2(1, 1);
        else
            self.transform.localScale = new Vector2(-1, 1);
    }
    /// <summary>
    /// will Check to see if the enemy is grounded
    /// </summary>
    /// <param name="capsuleCollider"></param>
    /// <param name="heightCheck"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public static bool IsGrounded(CapsuleCollider2D capsuleCollider,float heightCheck,LayerMask layerMask)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, heightCheck, layerMask);
        if (rayCastHit.collider != null)
        {
            return true;
        }
        else return false;

    }

}
