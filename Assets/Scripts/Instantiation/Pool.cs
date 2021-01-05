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
    private GameObject prefab;
    private Queue<GameObject> objects = new Queue<GameObject>();

    public Queue<GameObject> Objects { get => objects; }

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

        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }


    //Udemy Way
    public GameObject GetUsingList(string tag)
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
    public GameObject GetUsingList()
    {
        for(int i = 0; i < pooledItems.Count; i++)
        {
            if(pooledItems[i].activeInHierarchy==false)
            {
                return pooledItems[i];
            }

        }
        return null;
    }


//Jason Way Seems better no looking for string but not limited count also not going through a list everytime

    //public GameObject 


    //{
    //    if (objects.Count == 0)

    //        return objects.Dequeue();
    //}

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        Objects.Enqueue(objectToReturn);
    }
    private void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.SetActive(false);
        Objects.Enqueue(newObject);

        newObject.GetComponent<IGameObjectPooled>().GameObjectPool = this;

    }


}
