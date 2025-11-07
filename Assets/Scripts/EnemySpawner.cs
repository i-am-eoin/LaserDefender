using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;

    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i=0;i<waveConfig.GetNumberOfEnemies();i++) {
            GameObject enemyPrefab =Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity
            );
            enemyPrefab.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }


    void Update()
    {
        
    }
}
