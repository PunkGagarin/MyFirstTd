using System;
using System.Collections;
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

    public bool isBuffer = false;
    public float buffSpeed = 0f;
    public int buffType = 1;
    public float buffRadius = 10f;
    public float healAmount = 50f;
    public GameObject impactEffect;

    [Header("Unity Components")]
    public Image healthBar;

    public EnemyType type;
    [HideInInspector]
    public int minCount;
    public int constructValue;

    private Vector3 getCurrentPos() {
        return transform.position;
    }

    private void Start() {
        speed = startSpeed;
        health = startHealth;
        if (isBuffer)
            StartCoroutine(Buff(buffType));
    }

    public void DoBufferStuff() {
        if (isBuffer)
            StartCoroutine(Buff(buffType));
    }

    public IEnumerator Buff(int bufftype) {

        while (!isDead) {
            BuffAction(buffType);
            yield return new WaitForSeconds(1 / buffSpeed);
        }
    }

    private void BuffAction(int buffType) {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, buffRadius);
        foreach (Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                Heal(collider.transform);
            }
        }
    }

    private void Heal(Transform enemy) {
        Enemy en = enemy.GetComponent<Enemy>();

        //Todo: тоже непонятная проверка, зачем? связано с компонентом
        if (en != null) {
            en.Heal(healAmount);
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead) {
            Die();
        }
    }

    public void Heal(float _healAmount) {
        health += _healAmount;
        if (health > startHealth)
            health = startHealth;

        healthBar.fillAmount = health / startHealth;
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
