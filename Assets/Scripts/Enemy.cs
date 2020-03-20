using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    // Slow at E16
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public int reward = 10;
    public float startHealth = 100;
    private float health;

    public GameObject deathEffect;
    public bool isDead = false;

    [Header("Unity Components")]
    public Image healthBar;

    private Vector3 getCurrentPos() {
        return transform.position;
    }

    private void Start() {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        PlayerStats.Money += reward;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    public void Slow(float slowAmount) {
        speed = startSpeed * (1f - slowAmount);
    }
}
