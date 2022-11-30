using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(3);
    }

    void SpawnEnemyWave(int number)
    {
        for (int i = 0; i < number; i++)
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
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            SpawnEnemyWave(3);
        }
    }
}
