using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public GameManager Manager;
    public GameState State;

    private void Start()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    public void SwapState()
    {
        Manager.UpdateGameState(State);
    }
}
