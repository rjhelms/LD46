using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUpArray;

    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private Transform powerupContainer;

    [SerializeField]
    private float spawnFrequency;

    [SerializeField]
    private float spawnVariance;

    private bool hasChild;
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasChild)
        {
            if (powerupContainer.childCount == 0)
            {
                hasChild = false;
                nextSpawnTime = Time.time + (1 / (spawnFrequency * Random.Range(1 - spawnVariance, 1 + spawnVariance)));
            }
        } else if (Time.time > nextSpawnTime)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject toSpawn = powerUpArray[Random.Range(0, powerUpArray.Length)];
        Instantiate(toSpawn, spawnPosition.position, Quaternion.identity, powerupContainer);
        hasChild = true;
    }
}
