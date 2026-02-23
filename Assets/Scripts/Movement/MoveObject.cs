using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    Right,
    Left
}
public class MoveObject : MonoBehaviour
{
    [SerializeField] Direction DIR;
    [SerializeField] GameObject obj;
    [SerializeField] float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        if (speed < 0) speed = math.abs(speed);
        
        speed = (DIR == Direction.Right) ? speed : -speed;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        obj.transform.Translate(speed * dt, 0, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
            print("Destroy");
        }
    }
}
