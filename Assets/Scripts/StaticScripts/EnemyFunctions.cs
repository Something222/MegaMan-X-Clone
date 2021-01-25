using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFunctions  
{

   public static bool IsGrounded(CapsuleCollider2D capsuleCollider, float heightCheck, LayerMask platformLayerMask)
    {

        RaycastHit2D rayCastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, heightCheck, platformLayerMask);

        if (rayCastHit.collider != null)
        {
            return true;
        }
        else return false;


    }

    public static void TrackPlayer(GameObject player, GameObject self)
    {
        if (player.transform.position.x < self.transform.position.x)
            self.transform.localScale = new Vector2(1, 1);
        else
            self.transform.localScale = new Vector2(-1, 1);
    }

    public static void Jump(GameObject self, float moveSpeed,float jumpHeight)
    {
        if (self.transform.localScale.x == 1)
        {
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, jumpHeight));
        }
        else if (self.transform.localScale.x == -1)
        {
            self.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed,jumpHeight));
        }
    }

    public static void Shoot(GameObject prefab,float xPos,float yPos,float bulletSpeed,float scaleOnX,GameObject self)
    {
        if(scaleOnX==1)
        {
            var bullet = GameObject.Instantiate(prefab, new Vector3(self.transform.position.x-xPos, self.transform.position.y+yPos), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
        }
        else
        {
            var bullet = GameObject.Instantiate(prefab, new Vector3(self.transform.position.x+xPos, self.transform.position.y + yPos), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        }

    }

}
