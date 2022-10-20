using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState 
{
    Menu,
    Game,
    Lose,
    Quit
}
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;

    private int Money = 0;
    private int Health = 100;
    

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        UpdateGameState(State);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.Menu:
                SceneManager.LoadScene("Menu");
                break;

            case GameState.Game:
                SceneManager.LoadScene("MainScene");
                break;
            case GameState.Lose:
                SceneManager.LoadScene("GameOver");
                break;
            case GameState.Quit:
                Application.Quit();
                break;
            default:
                break;

        }
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            UpdateGameState(GameState.Quit);
        }
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



