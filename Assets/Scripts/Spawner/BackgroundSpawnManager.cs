using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundSpawnManager : Spawner
{
    [SerializeField] GameObject prefab;
    
    [Header("System Settings")]
    [SerializeField] Transform[] spawnPosition;
    
    
    [Header("Delay Settings")]
    [SerializeField] float delayMin;
    [SerializeField] float delayMax;
    
    
    [Header("Size Settings")]
    [SerializeField] float sizeMin;
    [SerializeField] float sizeMax;
    
    
    [Header("Showing")]
    [SerializeField] float timer;
    [SerializeField] float size;

    
    // Unity API
    void Start()
    {
        Spawn();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Spawn();
        }
    }

    // Self-Method

    // --- Init Method ---
    void InitTimer()
    {
        float randTime = Random.Range(delayMin, delayMax);
        timer = randTime;
        delay = timer;
    }

    void InitScaler(GameObject target)
    {
        float randScale = Random.Range(sizeMin, sizeMax);
        target.transform.localScale = new Vector3(randScale, randScale, 1);
        size = randScale;
    }

    
    // --- Inherit Methods ---
    public new virtual void StartGenerate()
    {
        base.StartGenerate();
    }

    protected override void Spawn()
    {
        int randIndex = Random.Range(0, spawnPosition.Length);
        GameObject inst = Instantiate(prefab, spawnPosition[randIndex].position, Quaternion.identity);
        
        InitScaler(inst);
        InitTimer();
    }
}