using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum GameState
{
    Prepare,
    InGame,
    Dead,
    Score
}

public class GameManager : MonoBehaviour
{
    int chance;

    [Header("輸入系統")]
    public GameState State;
    [SerializeField] Player player;
    [SerializeField] MonsterSpawner monsterSpawner;
    [SerializeField] EnergySpawner energySpawner;
    

    // Start is called before the first frame update
    void Start()
    {
        chance = player.chanceNum;
        
        SetGameState(GameState.Prepare);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左鍵
        {
            switch (State) // State Machine
            {
                case GameState.Prepare:
                    player.Go();
                    player.Jump();
                    player.shadow.gameObject.SetActive(true);

                    SetGameState(GameState.InGame);
                    break;

                case GameState.InGame:
                    player.Jump();

                    // Player -> trigger to "Monster" or "Boundary" -> Dead
                    break;

                case GameState.Dead:
                    break;

                case GameState.Score:
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(1)) // 右鍵
        {
            switch (State)
            {
                case GameState.InGame:
                    player.Attack(2); 
                    break;
            }
        }
    }

    // self-Method
    public void SetGameState(GameState state)
    {
        State = state;

        switch (State)
        {
            case GameState.Prepare:
                // Shadow
                player.shadow.SetActive(true);
                
                // State
                player.Prepare();
                monsterSpawner.StopGenerate();
                energySpawner.StopGenerate();
                break;

            case GameState.InGame:
                monsterSpawner.StartGenerate();
                energySpawner.StartGenerate();
                break;

            case GameState.Dead:
                // Shadow
                player.shadow.SetActive(false);
                
                // State
                chance--;
                player.chanceNum = chance;
                monsterSpawner.StopGenerate();
                energySpawner.StopGenerate();

                Invoke("GameCheck", 2.5f);
                break;

            case GameState.Score:
                break;
        }
    }

    void GameCheck()
    {
        if (chance > 0)
        {
            SetGameState(GameState.Prepare);
            return;
        }

        SetGameState(GameState.Score);
        return;
    }
}