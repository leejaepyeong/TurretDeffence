using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Enemy[] enemyPrefabs;

    public float lastSpawnTime = 0f; 
    public float spawnTime = 3f;

    


    private void Update()
    {
        if(!GameManager.instance.isGameOver && GameManager.instance.isWaveOver && lastSpawnTime <= 0)
        {
            lastSpawnTime = spawnTime;
            SpawnEnemy();
        }

        lastSpawnTime -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[(GameManager.instance.wave) % 4], transform.position, transform.rotation);

        GameManager.instance.SpawnWave();
    }

}
