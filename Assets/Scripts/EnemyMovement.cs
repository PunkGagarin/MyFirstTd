using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
        target = WayPoints.wayPoints[0];
    }

    private Vector3 getCurrentPos() {
        return transform.position;
    }

    void Update() {
        Vector3 direction = target.position - getCurrentPos();
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(getCurrentPos(), target.position) <= 0.4f) {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWayPoint() {

        if (waypointIndex >= WayPoints.wayPoints.Length - 1) {
            EndPath();
            return;
        }

        waypointIndex++;
        target = WayPoints.wayPoints[waypointIndex];
    }

    private void EndPath() {
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
        PlayerStats.Lives -= 1;
    }
}
