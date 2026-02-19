using UnityEngine;

public class IngredientSpawn : MonoBehaviour
{
    [SerializeField] private int timeToSpawn;
    [SerializeField] private ItemSurface surface;
    private ObjectPoolComp pool;
    private float time;

    void Start()
    {
        pool = GetComponent<ObjectPoolComp>();
        Debug.Log(pool != null);
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
        if (obj != null && surface.GetCurrentItem() == null)
        {
            obj.transform.position = transform.position;
            obj.SetActive(true);

            Debug.Log("Spawned ingredient: " + obj.name);
        }
    }
}