using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField ]private GameObject item;
    private LivingEntities enemyScript;

    private void Start()
    {
        item.SetActive(false);
        if (item.GetComponent<LivingEntities>() != null)
        enemyScript = item.GetComponent<LivingEntities>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (item.activeInHierarchy == false && collision.tag=="Spawner")
        {
            
            item.transform.position = gameObject.transform.position;
            item.SetActive(true);
            enemyScript.Respawn();

        }
    }
}
