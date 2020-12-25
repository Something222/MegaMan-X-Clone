using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    //basically give an object and how many can be spwned in game
    public GameObject prefab;
    public int amount;
}


public class Pool : MonoBehaviour
{
    public static Pool instance = null;
    [SerializeField] List<PoolItem> items;
    [SerializeField] List<GameObject> pooledItems;

    public GameObject Get(string tag)
    {
        for(int i=0;i<pooledItems.Count;i++)
        {
            if(tag==pooledItems[i].tag &&pooledItems[i].activeInHierarchy==false)
            {
                return pooledItems[i];
            }

        }
        return null;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        pooledItems = new List<GameObject>();

        foreach(PoolItem item in items)
        {
            for(int i=0;i<item.amount;i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
