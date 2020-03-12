using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooler : MonoBehaviour
{
    public static ObjectsPooler sharedInstance;
    public List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            Debug.Log("sharedinstance created");

        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
            Debug.Log("destroyed dup");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject instance = Instantiate(objectToPool) as GameObject;
            instance.gameObject.SetActive(false);
            pooledObjects.Add(instance);
            instance.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
