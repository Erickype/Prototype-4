using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    void SpawnEnemyWave()
    {
        for (int i = 0; i <= 3; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPos()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawny = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnX, 0, spawny);

        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
