using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    // Slow at E16
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int reward = 10;

    public GameObject deathEffect;

    private Vector3 getCurrentPos() {
        return transform.position;
    }

    private void Start() {
        speed = startSpeed;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        PlayerStats.Money += reward;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    public void Slow(float slowAmount) {
        speed = startSpeed * (1f - slowAmount);
    }
}
