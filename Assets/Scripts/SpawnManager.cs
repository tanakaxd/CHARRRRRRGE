using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject thingsPrefab;
    private List<Transform> spawnPositions = new List<Transform>();
    private float spawnInterval = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            //string number = i.ToString();
            //Debug.Log("SpawnPoint" + i);
            spawnPositions.Add(transform.Find("SpawnPoint"+i).transform); 
        }
        SpawnThings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnThings()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        Instantiate(thingsPrefab, spawnPositions[randomIndex].position, Quaternion.identity);
        Invoke("SpawnThings", spawnInterval);
    }
}
