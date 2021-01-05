using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float damage;
    // Start is called before the first frame update
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
        if (collision.tag == "Spawner")
        {
            gameObject.SetActive(false);
        }
    }
}
