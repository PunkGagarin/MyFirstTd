using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameEnded = false;
    void Update() {

        if (gameEnded)
            return;

        if (PlayerStats.Lives <= 0 
            //&& !gameEnded
            ) {
            GameOver();
            gameEnded = true;
        }
    }

    private void GameOver() {
        Debug.Log("Game over");
    }
}
