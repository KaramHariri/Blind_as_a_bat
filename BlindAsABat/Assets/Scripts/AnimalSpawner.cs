using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject FlyPrefab;
    public GameObject OwlPrefab;
    public float spawnWidth;
    public float spawnHeight;

    public float timePlayed = 0.0f;
     
    public int maxAmountOfFliesAlive;
    public int fliesCurrentlyAlive = 0;
    public int maxAmountOfOwlsAlive;
    public int owlsCurrentlyAlive = 0;
     
    public float fliesSpawnDelay = 3.0f;
    public float owlsSpawnDelay = 3.0f;
     
    public float fliesSpawnTimer = 0.0f;
    public float owlsSpawnTimer = 0.0f;

    void Start()
    {
        maxAmountOfFliesAlive = 3;
        maxAmountOfOwlsAlive = 1;
    }

    void Update()
    {
        timePlayed += Time.deltaTime;

        if(timePlayed > 200.0f)
        {
            maxAmountOfFliesAlive = 1;
            maxAmountOfOwlsAlive = 3;
        }
        else if(timePlayed >= 100.0f)
        {
            maxAmountOfFliesAlive = 2;
            maxAmountOfOwlsAlive = 2;
        }
        else if (timePlayed >= 50.0f)
        {
            maxAmountOfFliesAlive = 3;
            maxAmountOfOwlsAlive = 1;
        }
        else
        {
            maxAmountOfFliesAlive = 3;
            maxAmountOfOwlsAlive = 0;
        }

        if(maxAmountOfFliesAlive > fliesCurrentlyAlive)
        {
            fliesSpawnTimer = fliesSpawnTimer + Time.deltaTime;
            if(fliesSpawnTimer > fliesSpawnDelay)
            {
                SpawnFly();
            }
        }

        if (maxAmountOfOwlsAlive > owlsCurrentlyAlive)
        {
            owlsSpawnTimer = owlsSpawnTimer + Time.deltaTime;
            if (owlsSpawnTimer > owlsSpawnDelay)
            {
                SpawnOwl();
            }
        }

    }

    void SpawnFly()
    {
        float xPos = Random.Range(-spawnWidth, spawnWidth);
        float yPos = Random.Range(-spawnHeight, spawnHeight);

        Instantiate(FlyPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        fliesCurrentlyAlive++;
        fliesSpawnTimer = 0.0f;
    }

    void SpawnOwl()
    {
        float xPos = Random.Range(-spawnWidth, spawnWidth);
        float yPos = Random.Range(-spawnHeight, spawnHeight);

        Instantiate(OwlPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        owlsCurrentlyAlive++;
        owlsSpawnTimer = 0.0f;
    }

    public void FlyEaten()
    {
        fliesCurrentlyAlive--;
    }
}
