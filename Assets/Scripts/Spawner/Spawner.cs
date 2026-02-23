using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected float delay;
    
    
    protected void StartGenerate()
    {
        InvokeRepeating("Spawn", 0, delay);
    }
    
    public void StopGenerate()
    {
        CancelInvoke("Spawn");
    }
    
    protected abstract void Spawn();
}
