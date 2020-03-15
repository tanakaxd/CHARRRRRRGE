using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Transform> spawnPositions = new List<Transform>();


    public GameObject thingsPrefab;
    private float spawnInterval = 5;

    public GameObject bombPrefab;
    private float bombInterval = 20;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            //string number = i.ToString();
            //Debug.Log("SpawnPoint" + i);
            spawnPositions.Add(transform.Find("SpawnPoint"+i).transform); 
        }
        //SpawnThings();
        ShootBomb();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShootBomb()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        Instantiate(bombPrefab, spawnPositions[randomIndex].position, Quaternion.identity);
        Invoke("ShootBomb", bombInterval);
        bombInterval -= 2;
        bombInterval = Mathf.Clamp(bombInterval, 5, 20);
    }

    void SpawnThings()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        Instantiate(thingsPrefab, spawnPositions[randomIndex].position, Quaternion.identity);
        Invoke("SpawnThings", spawnInterval);
    }
}
