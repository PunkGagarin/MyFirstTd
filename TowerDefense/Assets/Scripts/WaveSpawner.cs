using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public GameObject[] enemies;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    private IDictionary<int, IDictionary<EnemyType, int>> waveTable;

    public void initWaveTable(Wave[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            IDictionary<EnemyType, int> dictionary = countWaveDictionary(waves[i].value);
        }
    }

    private IDictionary<EnemyType, int> countWaveDictionary(int value)
    {
        IDictionary<EnemyType, int> waveDictionary = new Dictionary<EnemyType, int>();

        while(value > 0)
        {
            EnemyType enemy = (EnemyType) new System.Random().Next(0, Enum.GetNames(typeof(EnemyType)).Length);

        }
        return waveDictionary;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
