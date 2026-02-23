using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergySpawner : Spawner
{
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] GameObject energyPrefab;

    void Start()
    {
        delay = 0.7f;
    }
    
    public new virtual void StartGenerate()
    {
        base.StartGenerate();
    }


    protected override void Spawn()
    {
        Vector3 randomSpawnPos = spawnPosition[Random.Range(0, spawnPosition.Length)].position;
        
        Instantiate(energyPrefab, randomSpawnPos, Quaternion.identity);
    }
}
