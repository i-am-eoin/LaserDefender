using UnityEngine;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }
    void Update()
    {
        EnemyMove();
    }
    
    void EnemyMove()
    {
        if (waypointIndex <= waypoints.Count)
        {
        var targetPosition = waypoints[waypointIndex].transform.position;        
        targetPosition.z = 0f;
        
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
        } else {
            Destroy(gameObject);
        }
    }
}