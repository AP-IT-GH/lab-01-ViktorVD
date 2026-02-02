using System;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    public GameObject[] Waypoints_Array;
    int currentWaypoint;
    int Distance_Check = 1;
    public float speed = 5.0f;
    public float rotSpeed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // zoek target en directie
        Vector3 target = Waypoints_Array[currentWaypoint].transform.position;
        Vector3 direction = target - transform.position;

        // beweging
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

        // als waypoint haalt
        if (Vector3.Distance(transform.position, target) < Distance_Check)
        {
            currentWaypoint++;
            if (currentWaypoint == Waypoints_Array.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
