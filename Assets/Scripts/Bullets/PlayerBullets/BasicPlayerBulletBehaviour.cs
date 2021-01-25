using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField]private float startingDamage;
    [SerializeField] private float moveSpeed;

    public float MoveSpeed { get => moveSpeed; }

    // Start is called before the first frame update
    private void Start()
    {
        damage = startingDamage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag != "Spawner")
        {
            var isliving = other.GetComponent<LivingEntities>();
            if (isliving != null)
            {
                damage = DamageDealer.DealDamageBullet(isliving, damage);
                if (damage <= 0)
                    gameObject.SetActive(false);
            }
            else
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

    protected virtual void OnEnable()
    {
        damage = startingDamage;
    }
}
