using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static bool IsGameOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
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

    void EndGame() {
        IsGameOver = true;
        IsGameOver = true;

        gameOverUI.SetActive(true);
    }

    public void WinLevel() {
        Debug.Log("LEVEL WON");
        IsGameOver = true;
        completeLevelUI.SetActive(true);
    }

}
