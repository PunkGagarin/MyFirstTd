using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive;

    public GameManager gameManager;

    public Wave[] waves;
    public GameObject[] enemies;

    public Transform spawnPoint;

    public float timeBetweenWaves = 8f;
    private float countdown = 10f;

    public Text waveCountdownText;
    private int waveIndex = 0;

    private ArrayList waveList = new ArrayList();

    public void initWaveList(Wave[] waves) {
        for (int i = 0; i < waves.Length; i++) {
            IDictionary<EnemyType, int> waveDictionary = createWaveDictionary(waves[i].value);
            waveList.Add(waveDictionary);
        }
    }

    private IDictionary<EnemyType, int> createWaveDictionary(int sumValue) {
        IDictionary<EnemyType, int> waveDictionary = new Dictionary<EnemyType, int>();
        Debug.Log("SUMVALUE: " + sumValue.ToString());

        System.Random rnd = new System.Random();
        while (sumValue > 0) {
            
            int random = rnd.Next(0, Enum.GetNames(typeof(EnemyType)).Length);
            Debug.Log(random.ToString());
            EnemyType type = (EnemyType)random;
            Enemy enemy = enemies[0].GetComponent<Enemy>();
            for (int i = 0; i < enemies.Length; i++) {
                Debug.Log(enemies[i].GetComponent<Enemy>().type.Equals(type) && enemies[i].GetComponent<Enemy>().constructValue <= sumValue);
                Debug.Log("Type: " + enemies[i].GetComponent<Enemy>().type + " Random type: " + type + " Value : " + enemies[i].GetComponent<Enemy>().constructValue + " SumValue: " + sumValue);
                if (enemies[i].GetComponent<Enemy>().type.Equals(type) && enemies[i].GetComponent<Enemy>().constructValue <= sumValue) {
                    enemy = enemies[i].GetComponent<Enemy>();
                }
            }

            if (sumValue >= enemy.constructValue) {
                if (waveDictionary.TryGetValue(enemy.type, out int value)) {
                    waveDictionary[type] =  value + 1;
                    sumValue -= enemy.constructValue;
                    Debug.Log(" INCREMENT!!!! SUMVALUE AFTER ITERATION: " + sumValue + " LAST ENEMY VALUE: " + enemy.constructValue + " " + Enum.GetName(typeof(EnemyType), enemy.type));
                } else {
                    waveDictionary.Add(enemy.type, 1);
                    sumValue -= enemy.constructValue;
                    Debug.Log("NEW !!!! SUMVALUE AFTER ITERATION: " + sumValue + " LAST ENEMY VALUE: " + enemy.constructValue + " " + Enum.GetName(typeof(EnemyType), enemy.type));
                }
            } else
                break;
        }
        return waveDictionary;
    }

    private void Start() {
        EnemiesAlive = 0;
        initWaveList(waves);
        for (int i = 0; i < waveList.Count; i++) {
            IDictionary<EnemyType, int> item = (IDictionary<EnemyType, int>)waveList[i];
            StringBuilder sb = new StringBuilder();
            sb.Append("Wave number: " + i);
            foreach (KeyValuePair<EnemyType, int> kvp in item) {
                sb.Append(" Key= " + kvp.Key + " Value = " + kvp.Value + " ");
            }
            Debug.Log(sb.ToString());
        }
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

    IEnumerator SpawnWaveFromList() {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++) {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }
        waveIndex++;
    }

    IEnumerator SpawnWave() {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++) {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
