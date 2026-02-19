using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawn : MonoBehaviour
{
    [SerializeField] private int timeToSpawn;
    private ObjectPoolComp pool;
    private float time;

    void Start()
    {
        pool = GetComponent<ObjectPoolComp>();
        time = 0;
        SpawnOne();
    }

    void Update()
    {
        if (time < timeToSpawn)
            time += Time.deltaTime;
        else
        {
            time = 0;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        GameObject obj = pool.getObject();
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
    }
}