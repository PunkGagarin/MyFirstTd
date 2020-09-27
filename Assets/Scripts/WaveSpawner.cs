using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

public class WaveSpawner : MonoBehaviour {
    [HideInInspector]
    public static int EnemiesAlive;

    public GameManager gameManager;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 8f;
    private float countdown = .5f;

    public Text waveCountdownText;
    private int waveIndex = 0;

    private void Start() {
        EnemiesAlive = 0;
    }

    private void Update() {

        if (EnemiesAlive > 0) {
            return;
        }

        if (waveIndex == waves.Length) {

            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave() {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.getEnemiesCount();

        for (int i = 0; i < wave.enemiesDict.Length; i++) {
            Wave.EnemiesDict currentEnemy = wave.enemiesDict[i];
            for (int j = 0; j < currentEnemy.count; j++) {
                SpawnEnemy(currentEnemy.enemy);
                float timeBetweenEnemies = j == currentEnemy.count - 1 ?
                    currentEnemy.timeBeforeNextEnemy != 0 ? currentEnemy.timeBeforeNextEnemy : 1 : currentEnemy.rate;
                yield return new WaitForSeconds(1 / timeBetweenEnemies );
            }
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
