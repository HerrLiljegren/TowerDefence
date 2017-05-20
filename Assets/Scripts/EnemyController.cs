using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 300f;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.points[waypointIndex];
        transform.position = target.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= 5f)
        {
            GetNextWaypoint();
        }

        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        target = Waypoints.points[++waypointIndex];
    }
}
