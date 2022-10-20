using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public GameManager Manager;
    public GameState State;

    public void SwapState()
    {
        Manager.UpdateGameState(State);
    }
}
