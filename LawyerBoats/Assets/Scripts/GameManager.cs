using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    private int Money = 0;
    private int Health = 100;
    public enum GameState {
    Menu,
    Game,
    Win,
    Lose
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Menu);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Game:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }



    public void addHealth()
    {
        Health++;
    }

    public void loseHealth(int health)
    {
        Health -= health;
    }

    public int getHealth()
    {
        return Health;
    }

    public void addMoney()
    {
        Money++;
    }

    public void loseMoney(int money)
    {
        Money -= money;
    }

    public int getMoney()
    {
        return Money;
    }
}



