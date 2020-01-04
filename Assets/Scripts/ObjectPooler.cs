using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<IPooleable, Queue<IPooleable>> pools = new Dictionary<IPooleable, Queue<IPooleable>>();
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    public T GetObject<T>(T prefab) where T : MonoBehaviour, IPooleable
    {
        Queue<IPooleable> p;
        T obj;
        bool existPool = pools.TryGetValue(prefab, out p);
        if (existPool)
        {
            obj = p.Peek() as T;
            if (obj.IsBeingUsed())
            {
                obj = Instantiate<T>(prefab);
                p.Enqueue(obj);
            }
            else
            {
                p.Dequeue();
                p.Enqueue(obj);
            }
        }
        else
        {
            p = new Queue<IPooleable>();
            pools[prefab] = p;
            obj = Instantiate(prefab);
            p.Enqueue(obj);
        }

        obj.OnLeavePool();
        return obj;
    }
 
}


public interface IPooleable
{
    void OnEnterPool();

    void OnLeavePool();

    bool IsBeingUsed();
}