using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int reward = 10;

    public GameObject deathEffect;

    private Transform target;
    private int waypointIndex = 0;

    private Vector3 getCurrentPos()
    {
        return transform.position;
    }


    void Start()
    {
        target = WayPoints.wayPoints[0];
    }

    void Update()
    {
        Vector3 direction = target.position - getCurrentPos();
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(getCurrentPos(), target.position) <= 0.4f) 
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {

        if(waypointIndex >= WayPoints.wayPoints.Length -1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = WayPoints.wayPoints[waypointIndex];
    }

    private void EndPath() {
        Destroy(gameObject);
        PlayerStats.Lives -= 1;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    private void Die() {
        PlayerStats.Money += reward;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
