using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameController gameController;

    void OnMouseDown()
    {
        gameController.StartGame();
    }
}
