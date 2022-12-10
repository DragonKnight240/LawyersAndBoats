using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    [SerializeField] TextMeshProUGUI waveTimeUI;
    public GameObject waveTimePanel;

    [SerializeField] GameObject pauseMenu;
    public GameObject optionsMenu;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        
    }

    public bool ToggleWaveTimePanel()
    {
        waveTimePanel.SetActive(!waveTimePanel.activeSelf);
        return waveTimePanel.activeSelf;
    }

    public void UpdateWaveTimeUI(int amt)
    {
        waveTimeUI.text = "Time to next wave: " + amt;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
        SceneManager.LoadScene("CamsTestLevel");
        GameManager.Instance.State = GameState.Game;
    }
}
