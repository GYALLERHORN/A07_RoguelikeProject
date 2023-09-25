using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum ePoolType
{
    SoundSource,
    Status,
    Inventory,
    Store,
    TextImage,
}

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public ePoolType type;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private int _resizeSize;
    [SerializeField] private int _maxSize;
    public List<Pool> pools;
    public Dictionary<ePoolType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<ePoolType, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj;
                obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectsPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.type, objectsPool);
        }
    }

    public GameObject SpawnFromPool(ePoolType tag)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);

        int count = pools.Find(x => x.type == tag).size;
        while (obj.activeInHierarchy)
        {
            if (count < 0)
            {
                count = UpsizePool(tag, _resizeSize);
                if (count == pools.Find(x => x.type == tag).size)
                {
                    obj = null;
                    break;
                }
            }
            else
            {
                count--;

                obj = poolDictionary[tag].Dequeue();
                poolDictionary[tag].Enqueue(obj);
            }
        }
        return obj;
    }

    public int UpsizePool(ePoolType type, int size)
    {
        var poolQueue = poolDictionary[type];

        int poolIndex = pools.FindIndex(x => x.type == type);
        Pool targetPool = pools[poolIndex];

        if (targetPool.size >= _maxSize)
            return targetPool.size;
        else
        {
            targetPool.size += size;
            pools[poolIndex] = targetPool;

            for (int i = 0; i < size; ++i)
            {
                GameObject obj = Instantiate(targetPool.prefab);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }

            return targetPool.size;
        }
    }

    public int DownsizePool(ePoolType type, int size)
    {
        var poolQueue = poolDictionary[type];

        int poolIndex = pools.FindIndex(x => x.type == type);
        Pool targetPool = pools[poolIndex];
        targetPool.size -= size;
        pools[poolIndex] = targetPool;

        for (int i = 0; i < size; ++i)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(false);
            Destroy(obj);
        }

        return targetPool.size;
    }
}
