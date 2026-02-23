using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] bool allowRun = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowRun)
        {
            timer += Time.deltaTime;
        }
    }
    
    
    // self-Method
    public void Init()
    {
        timer = 0;
        allowRun = false;
    }

    public void Run()
    {
        allowRun = true;
    }

    void Pause()
    {
        allowRun = false;
    }
}
