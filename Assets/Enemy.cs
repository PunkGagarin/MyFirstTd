using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;

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

        if(wavePointIndex >= WayPoints.wayPoints.Length -1)
        {
            Destroy(gameObject);
            return;
        }

        wavePointIndex++;
        target = WayPoints.wayPoints[wavePointIndex];
    }
}
