using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public GameState State;

    private int Money = 0;
    private int Health = 100;

    [SerializeField] int startingMoney = 50;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;

    public int enemyCount = 0;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Init()
    {
        Money = startingMoney;
        UpdateGameState(State);
        UpdateMoneyUI();
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
        UpdateHealthUI();
        Health++;
    }

    public void loseHealth(int health)
    {
        UpdateHealthUI();
        Health -= health;
    }

    public int getHealth()
    {
        return Health;
    }

    public void addMoney()
    {
        UpdateMoneyUI();
        Money++;
    }

    public void addMoney(int amt)
    {
        UpdateMoneyUI();
        Money += amt;
    }

    public void loseMoney(int money)
    {
        Money -= money;
    }

    public int getMoney()
    {
        return Money;
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + Health;
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "Coins: " + Money;
    }
}



