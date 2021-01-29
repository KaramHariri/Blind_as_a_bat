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

    //Camera stuff for spawn
    private float top = 0f;
    private float right = 0f;
    private float left = 0f;
    private float bottom = 0f;

    SoundManager soundManager = null;

    [SerializeField]
    private float difficultyEasy = 20f;
    [SerializeField]
    private float difficultyMedium = 60f;
    [SerializeField]
    private float difficultyHard = 120f;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Start()
    {
        FindSpawnBounds();

        maxAmountOfFliesAlive = 3;
        maxAmountOfOwlsAlive = 1;
    }

    void Update()
    {
        timePlayed += Time.deltaTime;

        if(timePlayed > difficultyHard)
        {
            maxAmountOfFliesAlive = 1;
            maxAmountOfOwlsAlive = 3;
        }
        else if(timePlayed >= difficultyMedium)
        {
            maxAmountOfFliesAlive = 2;
            maxAmountOfOwlsAlive = 2;
        }
        else if (timePlayed >= difficultyEasy)
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
        float xPos = Random.Range(bottom + 0.5f, top - 0.5f);
        float yPos = Random.Range(left + 0.5f, right - 0.5f);

        Instantiate(FlyPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        fliesCurrentlyAlive++;
        fliesSpawnTimer = 0.0f;
    }

    void SpawnOwl()
    {
        Instantiate(OwlPrefab, Vector3.zero, Quaternion.identity);
        owlsCurrentlyAlive++;
        owlsSpawnTimer = 0.0f;

        soundManager.PlaySound("Owl");
    }

    public void FlyEaten()
    {
        fliesCurrentlyAlive--;
    }

    void FindSpawnBounds()
    {
        top = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).y;
        right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).x;
        left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }

    public void OwlDied()
    {
        owlsCurrentlyAlive--;
    }
}
