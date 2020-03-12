using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrack : MonoBehaviour
{
    public GameObject infantryPrefab;

    private float spawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInfantry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnInfantry()
    {
        GameObject infantry = Instantiate(infantryPrefab, transform.position, transform.rotation) as GameObject;
        Engine.instance.Infantiries.Add(infantry);
        infantry.transform.SetParent(transform);
        Invoke("SpawnInfantry", spawnInterval);
    }
}
