using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Spawner
{
    [SerializeField] Transform[] spawnPosition;

    [SerializeField] GameObject[] monster_75pct;
    [SerializeField] GameObject[] monster_25pct;

    void Start()
    {
        this.delay = 1.2f;
    }
    
    // self-Method
    public new virtual void StartGenerate()
    {
        base.StartGenerate();
    }

    protected new virtual void StopGenerate()
    {
        base.StopGenerate();
    }
    

    protected override void Spawn()
    { // generate once monster
        
        Vector3 randomSpawnPos = spawnPosition[Random.Range(0, spawnPosition.Length)].position;
        void INST(GameObject[] monster_pct)
        {
            int tempIdx = Random.Range(0, monster_pct.Length);
            
            Instantiate(monster_pct[tempIdx],
                randomSpawnPos,
                Quaternion.identity);
        }
        

        int randomPCT = Random.Range(0, 100);
        if (randomPCT < 75) // 75%
        {
            INST(monster_75pct);
        }
        else // 25%
        {
            INST(monster_25pct);
        }
    }
}