using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageDealer 
{
    public static void DealDamage(LivingEntities other,float damage)
    {
        if (!other.invicibility)
        {
            other.invicibility = true;
            other.StartCoroutine(other.TurnOffInvicibility());
            other.health -= damage;
            if (other.health <= 0)
            {
                other.Die();
            }
        }
    }

    public static float DealDamageBullet(LivingEntities other, float damage)
    {
        float returningDamage=0;
        if (!other.invicibility)
        {
            other.invicibility = true;
            other.StartCoroutine(other.TurnOffInvicibility());
            returningDamage = damage - other.health;
            other.health -= damage;
            if (other.health <= 0)
            {
                other.Die();
            }
        }
        return returningDamage;
    }
}
