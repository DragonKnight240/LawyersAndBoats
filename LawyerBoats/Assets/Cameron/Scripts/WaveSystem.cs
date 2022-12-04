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

    [SerializeField] float timeToNextWave = 5.0f;
    float currentTimeToNextWave = 0;

    bool spawning = true;

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
        currentTimeToNextWave = timeToNextWave;
    }

    void Update()
    {
        if (!spawning)
        {
            return;
        }
        if (currentWave == amountOfWaves && EnemiesInWave() <= 0)
        {
            spawning = false;
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
        if (currentTimeToNextWave > 0)
        {
            currentTimeToNextWave -= Time.deltaTime;
        }
        else
        {
            if (UIManager.Instance.waveTimePanel.activeSelf == true)
            {
                UIManager.Instance.ToggleWaveTimePanel();
            }
        }

        if (UIManager.Instance.waveTimePanel.activeSelf == true)
        {
            UIManager.Instance.UpdateWaveTimeUI((int)currentTimeToNextWave);
        }

        if (timeSinceLastSpawn > timeBetweenSpawns && spawningEnemies && currentTimeToNextWave <= 0)
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

            if (EnemiesInWave() <= 0 && GameManager.Instance.enemyCount == 0 && spawning)
            {
                NextWave();
            }
            else if (currentWave == amountOfWaves)
            {
                spawning = false;
                return;
            }

            if (waves[currentWave].EnemiesToSpawn[currentEnemy].enemiesInWave <= 0 && currentEnemy < waves[currentWave].EnemiesToSpawn.Length - 1 && spawning)
            {
                currentEnemy++;
            }



        }
    }

    void NextWave()
    {

        ++currentWave;
        if (currentWave == amountOfWaves)
        {
            spawning = false;
            return;
        }
        /*if (currentWave == amountOfWaves)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            return;
        }*/
        currentEnemy = 0;
        timeBetweenSpawns = waveTime / EnemiesInWave();
        currentTimeToNextWave = timeToNextWave;
        UIManager.Instance.ToggleWaveTimePanel();
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
        GameManager.Instance.enemyCount++;
    }

    
}
