using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    public GameObject[] powerUpsPrefabs;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame updates
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    void SpawnEnemyWave(int number)
    {
        for (int i = 0; i < number; i++)
        {
            int randomPos = Random.Range(0, enemiesPrefabs.Length);
            Debug.Log("Length: " + enemiesPrefabs.Length + " Random: " + randomPos);

            Instantiate(enemiesPrefabs[randomPos], GenerateRandomPos(), enemiesPrefabs[randomPos].transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        int randomPos = Random.Range(0, powerUpsPrefabs.Length);

        Instantiate(powerUpsPrefabs[randomPos], GenerateRandomPos(), powerUpsPrefabs[randomPos].transform.rotation);
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
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }
}
