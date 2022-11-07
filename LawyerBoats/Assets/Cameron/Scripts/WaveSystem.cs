using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] float waveTime = 60f;
    [SerializeField] GameObject winScreen;
    float timeBetweenSpawns;
    float timeSinceLastSpawn = 0f;
    public Wave[] waves;
    int currentWave = 0;
    int amountOfWaves;
    int currentEnemy = 0;
    bool spawningEnemies = true;
    [SerializeField] Transform portal;

    [Serializable]
    public struct Wave
    {
        public Enemy[] EnemiesToSpawn;
    }

    [Serializable]
    public struct Enemy
    {
        public GameObject enemy;
        public int enemiesInWave;
    }

    private void Awake()
    {
        timeBetweenSpawns = waveTime / EnemiesInWave();
        amountOfWaves = waves.Length;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawns && spawningEnemies)
        {
            timeSinceLastSpawn = 0f;
            /*do
            {
                rand = UnityEngine.Random.Range(0, waves[currentWave].EnemiesToSpawn.Length);
            } while (waves[currentWave].EnemiesToSpawn[rand].enemiesInWave <= 0);*/

            if (waves[currentWave].EnemiesToSpawn[currentEnemy].enemiesInWave > 0)
            {
                SpawnEnemy(waves[currentWave].EnemiesToSpawn[currentEnemy].enemy);
                waves[currentWave].EnemiesToSpawn[currentEnemy].enemiesInWave--;
            }

            if (EnemiesInWave() <= 0)
            {
                NextWave();
            }

            if (waves[currentWave].EnemiesToSpawn[currentEnemy].enemiesInWave <= 0)
            {
                currentEnemy++;
            }

            if (currentWave >= waves.Length)
            {
                spawningEnemies = false;
            }


        }
    }

    void NextWave()
    {
        ++currentWave;
        /*if (currentWave == amountOfWaves)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            return;
        }*/
        timeBetweenSpawns = waveTime / EnemiesInWave();
    }

    int EnemiesInWave()
    {
        int count = 0;
        foreach (Enemy enemy in waves[currentWave].EnemiesToSpawn)
        {
            count += enemy.enemiesInWave;
        }
        return count;
    }

    void SpawnEnemy(GameObject Enemy)
    {
        GameObject spawnedEnemy = Instantiate(Enemy, portal);
        spawnedEnemy.transform.parent = null;
    }

}
