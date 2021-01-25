using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageDealer 
{
    public static void DealDamage(LivingEntities other,float damage)
    {
        if (!other.invicibility)
        {
            other.TakeDamage((int)damage);         
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
            returningDamage = damage - other.health;
            other.TakeDamage((int)damage);
            if (other.health <= 0)
            {
                other.Die();
            }
        }
        return returningDamage;
    }
}
