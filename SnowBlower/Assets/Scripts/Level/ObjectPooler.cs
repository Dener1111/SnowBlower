using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler inst;

    //
    public Transform parent;
    [Space]
    //

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        if(inst == null)
            inst = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool item in pools)
            CreatePool(item);
    }

    void CreatePool(Pool pool)
    {
        Queue<GameObject> oPool = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            //
            obj.transform.parent = parent;
            //
            obj.SetActive(false);
            oPool.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, oPool);
    }

    public GameObject Spawn (string tag, Vector3 pos, Quaternion rot)
    {
        if(!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        poolDictionary[tag].Enqueue(obj);

        obj.transform.position = pos;
        obj.transform.rotation = rot;

        IPoolable p = obj.GetComponent<IPoolable>();
        if(p != null)
            p.Activate();

        return obj;
    }
}
