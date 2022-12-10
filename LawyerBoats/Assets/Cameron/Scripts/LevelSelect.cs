using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] List<string> levelNames;

    public void Level1()
    {
        SceneManager.LoadScene(levelNames[0]);
        GameManager.Instance.State = GameState.Game;
    }

    public void Level2()
    {
        SceneManager.LoadScene(levelNames[1]);
        GameManager.Instance.State = GameState.Game;
    }

    public void Level3()
    {
        SceneManager.LoadScene(levelNames[2]);
        GameManager.Instance.State = GameState.Game;
    }

    public void Level4()
    {
        SceneManager.LoadScene(levelNames[3]);
        GameManager.Instance.State = GameState.Game;
    }

    public void Level5()
    {
        SceneManager.LoadScene(levelNames[4]);
        GameManager.Instance.State = GameState.Game;
    }

    public void Level6()
    {
        SceneManager.LoadScene(levelNames[5]);
        GameManager.Instance.State = GameState.Game;
    }
}
