using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static bool IsGameOver;
    public GameObject gameOverUI;


    private void Start() {
        IsGameOver = false;
    }
    void Update() {

        if (IsGameOver)
            return;

        if (Input.GetKeyDown("e"))
            EndGame();

        if (PlayerStats.Lives <= 0
            //&& !gameEnded
            ) {
            EndGame();
        }
    }

    private void EndGame() {
        IsGameOver = true;
        IsGameOver = true;

        gameOverUI.SetActive(true);
    }
}
