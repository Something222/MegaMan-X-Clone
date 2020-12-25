using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Spawner")
        {
            var isliving = other.GetComponent<LivingEntities>();
            if (isliving != null)
            {
                DamageDealer.DealDamage(isliving, damage);
            }
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Spawner")
        {
            gameObject.SetActive(false);
        }
    }
}
