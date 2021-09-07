using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Enemy[] bossPrefabs;
    public Transform[] SpawnPosition;

    private float lastSpawnTime = 0f; 
    private float spawnTime = 6f;

    

    private void Update()
    {
        if(!GameManager.instance.isGameOver && !GameManager.instance.isWaveOver && lastSpawnTime <= 0)
        {

            if (!GameManager.instance.isBossSpawn)
            {
                lastSpawnTime = spawnTime;
                SpawnEnemy();
            }
            else
            {
                SpawnBossEnemy();
            }
            
        }

        lastSpawnTime -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < SpawnPosition.Length; i++)
        {
            Instantiate(enemyPrefabs[(GameManager.instance.wave - 1) % 5], SpawnPosition[i].position, SpawnPosition[i].rotation);
        }


        
        GameManager.instance.SpawnWave();
    }

    void SpawnBossEnemy()
    {
        for (int i = 0; i < SpawnPosition.Length; i++)
        {
            Instantiate(bossPrefabs[((GameManager.instance.wave) / 5 - 1) % 4], SpawnPosition[i].position, SpawnPosition[i].rotation);
        }

        GameManager.instance.SpawnWave();
    }

}
