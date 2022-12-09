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
    
    public void MainMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
