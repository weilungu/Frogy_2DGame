using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Property
    [Header("系統輸入")]
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameManager GM;


    [Header("手動輸入")] 
    [SerializeField] int speed = 5;
    [SerializeField] public int chanceNum;
    [SerializeField] Vector2 startPosition;
    [SerializeField] public GameObject shadow;

    
    [Header("能量控制")]
    [SerializeField] GameObject[] EnergyPoint;
    [SerializeField] int _energy;
    [SerializeField] int MaxEnergy = 6;
    
    
    [Header("子彈管理")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPosition;
    
    public int Energy
    {
        get => _energy;
        set => _energy = Mathf.Clamp(value, 0, MaxEnergy);
        
    }

    // --- Unity API ---
    void Start()
    {
        gameObject.transform.position = startPosition;
    }

    void Update()
    {
        // 影子渲染
        shadow.gameObject.transform.position
            = new Vector3(transform.position.x, -4.5f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary") ||
            other.gameObject.CompareTag("Monster"))
        {
            if (GM.State == GameState.InGame)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                
                anim.Play("Hert");
                GM.SetGameState(GameState.Dead);
            }
        }

        if (other.gameObject.CompareTag("Ceiling"))
        {
            print("天花板");
        }

        if (other.gameObject.CompareTag("Energy"))
        {
            SetEnergy(1);
            
            Destroy(other.gameObject);
        }
    }
    
    
    // --- self-Method ---
    
    // --- Energy ---
    void SetEnergy(int num)
    {
        for (int i = 0; i < EnergyPoint.Length; i++)
        {
            EnergyPoint[i].SetActive(false);
        } // Close All Energy Point
        
        Energy += num;
        
        for (int i = 0; i < Energy; i++)
        {
            EnergyPoint[i].SetActive(true);
        } // 更新 Energy
    }

    public void EnergyFull()
    {
        SetEnergy(MaxEnergy);
        
        for (int i = 0; i < EnergyPoint.Length; i++)
        {
            EnergyPoint[i].SetActive(true);
        } // Open All Energy Point
        
    }

    // --- state ---
    public void Attack(int consumeEnergy)
    {
        if (Energy >= consumeEnergy)
        {
            anim.SetTrigger("Attack");
            
            Instantiate(bullet, shootPosition.position, Quaternion.identity);
            SetEnergy(-consumeEnergy);
        }
    }
    
    public void Prepare()
    {
        SetEnergy(MaxEnergy);
        
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.transform.position = startPosition;

        anim.SetTrigger("Wait");
        print("Prepared");
    }

    public void Go()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        anim.SetTrigger("Fall");
        print("Running");
    }

    public void Jump()
    {
        if (Energy > 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(0, speed);

            SetEnergy(-1);

            anim.SetTrigger("Jump");
            particle.Play();

            print("Jumped");
        }
        else
        {
            print("能量不足");
        }
    }
}