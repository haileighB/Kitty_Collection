using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float speed = 1f;
    [SerializeField] bool rotateOnSwitch;
    int currentWaypointIndex = 0;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0; //go back to 0 after last waypoint
        }
        if (rotateOnSwitch)
        {
            //look towards waypoint
            transform.rotation = Quaternion.LookRotation(waypoints[currentWaypointIndex].transform.position - transform.position) * Quaternion.AngleAxis(-90, Vector3.up);
        }
        //move towards waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
