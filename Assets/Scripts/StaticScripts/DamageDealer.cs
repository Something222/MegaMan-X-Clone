using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageDealer 
{
    public static void DealDamage(LivingEntities other,float damage)
    {
        other.health -= damage;
        if(other.health<=0)
        {
            other.Die();
        }
    }
}
