using UnityEngine;
using System.Collections.Generic;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    int waypointIndex = 0;

    void Start()
    {
        transform.position = waveConfig.GetWaypoints()[waypointIndex].transform.position;

    }
    void Update()
    {
        EnemyMove();
    }
    
    void EnemyMove()
    {
        if (waypointIndex < waveConfig.GetWaypoints().Count)
        {
        var targetPosition = waveConfig.GetWaypoints()[waypointIndex].transform.position;        
        targetPosition.z = 0f;
        
        var movementThisFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
        } else {
            Destroy(gameObject);
        }
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}