﻿using UnityEngine;

public class WayPoints : MonoBehaviour
{

    public static Transform[] wayPoints;

    public void Awake()
    {
        wayPoints = new Transform[transform.childCount];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
