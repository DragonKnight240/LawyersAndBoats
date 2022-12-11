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
    Options,
    LevelSelect,
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

    internal float DamageMultiplier = 1;
    internal float TimerForMultiplier = 0;
    internal float TimeForMultiplier = 0;
    internal bool UsingMultiplier = false;



    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            _instance.healthText = healthText;
            _instance.moneyText = moneyText;
            _instance.startingMoney = startingMoney;
            _instance.Init();
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        if (State == GameState.Game)
        {
            Money = startingMoney;
            UpdateMoneyUI();
        }
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
                break;
            case GameState.Lose:
                SceneManager.LoadScene("GameOver");
                break;
            case GameState.LevelSelect:
                SceneManager.LoadScene("LevelSelect");
                break;
            case GameState.Options:
                SceneManager.LoadScene("OptionsMenu");
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
        if(State == GameState.Game)
        {
            if (UsingMultiplier)
            {
                TimerForMultiplier += Time.deltaTime;

                if (TimerForMultiplier >= TimeForMultiplier)
                {
                    TimerForMultiplier = 0;
                    UsingMultiplier = false;
                    DamageMultiplier = 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.P) && !UIManager.Instance.optionsMenu.activeSelf)
            {
                UIManager.Instance.TogglePauseMenu();
                ToggleTime();
            }
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
        Health -= Mathf.RoundToInt(health * DamageMultiplier);
        if (Health <= 0)
        {
            UIManager.Instance.loseScreen.SetActive(true);
            ToggleTime();
        }
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
        Money += amt;
        UpdateMoneyUI();
    }

    public void loseMoney(int money)
    {
        Money -= money;
        UpdateMoneyUI();
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

    public void ToggleTime()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    public void Level1()
    {
        GameManager.Instance.State = GameState.Game;
        SceneManager.LoadScene("CamsTestLevel");
    }
}



