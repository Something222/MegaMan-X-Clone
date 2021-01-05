using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{

    [SerializeField] private T prefab;
    [SerializeField] private int amount;

    public static GenericObjectPool<T> Instance { get; private set; }
    public Stack<T> Objects { get => objects;  }

    [SerializeField] private Stack<T> objects = new Stack<T>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
     for (int i=0;i<amount;i++)
        {
            var newObject = GameObject.Instantiate(prefab);
           
            newObject.gameObject.SetActive(false);
           
        }
    }
    public T GetAndAdd()
    {
        if (Objects.Count == 0)
            AddObjects(1);
        return Objects.Pop();

    }
    public T Get()
    {
   
        if (Objects.Count == 0)
            return null;
        return Objects.Pop();
    }
  public void ReturnToPool(T objectToReturn)
    {
       // objectToReturn.gameObject.SetActive(false);
        Objects.Push(objectToReturn);
    }
    private void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        Objects.Push(newObject);
    }

}
