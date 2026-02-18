using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolComp : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool dynamicGrowth;

    private List<GameObject> objPool;

    void Start()
    {
        objPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(objPrefab);
            obj.SetActive(false);
            objPool.Add(obj);
        }//end for
    }

    public GameObject getPrefab()
    {
        return objPrefab;
    }

    public GameObject getObject(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject objToReturn = null;

        for (int i = 0; i < objPool.Count; i++)
        {
            if (!objPool[i].activeInHierarchy)
            {
                objToReturn = objPool[i];
                break;
            }
        }

        if (objToReturn == null && dynamicGrowth)
        {
            objToReturn = Instantiate(objPrefab);
            objPool.Add(objToReturn);
        }

        if (objToReturn != null)
        {
            // Apply position and rotation BEFORE activating
            objToReturn.transform.position = spawnPosition;
            objToReturn.transform.rotation = spawnRotation;
            objToReturn.SetActive(true);
        }

        return objToReturn;
    }

}
