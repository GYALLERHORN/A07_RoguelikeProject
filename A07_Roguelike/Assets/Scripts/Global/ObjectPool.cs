using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum ePoolType
{
    SoundSource,
    Projectile = 20,
}


public enum eAttackType
{
    Projectile = 20,
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
    [SerializeField] private Transform _root;
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
                if (_root == null)
                    obj = Instantiate(pool.prefab);
                else
                    obj = Instantiate(pool.prefab, _root);
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

        int size = pools.Find(x => x.type == tag).size;
        int count = size;

        while (obj.activeInHierarchy)
        {
            if (count < 0)
            {
                if (size == _maxSize)
                {
                    obj = null;
                    break;
                }
                count = UpsizePool(tag, _resizeSize);
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

    public int UpsizePool(ePoolType type, int resize)
    {
        var poolQueue = poolDictionary[type];

        int poolIndex = pools.FindIndex(x => x.type == type);
        Pool targetPool = pools[poolIndex];

        if (targetPool.size < _maxSize)
        {
            int temp = Mathf.Min(targetPool.size + resize, _maxSize);
            int limit = temp - targetPool.size;

            targetPool.size = temp;
            pools[poolIndex] = targetPool;

            for (int i = 0; i < limit; ++i)
            {
                GameObject obj;
                if (_root == null)
                    obj = Instantiate(targetPool.prefab);
                else
                    obj = Instantiate(targetPool.prefab, _root);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }
        }
        return targetPool.size;
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
