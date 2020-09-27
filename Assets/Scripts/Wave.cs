using UnityEngine;

[System.Serializable]
public class Wave {

    [System.Serializable]
    public struct EnemiesDict {
        public GameObject enemy;
        public int count;
        public float rate;
        public float timeBeforeNextEnemy;
    }
    public EnemiesDict[] enemiesDict;

    public int getEnemiesCount() {
        int count = 0;
        for (int i = 0; i < enemiesDict.Length; i++) {
            count += enemiesDict[i].count;
        }
        return count;
    }
}
