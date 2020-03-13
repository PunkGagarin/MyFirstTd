using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static bool IsGameOver;

    public GameObject gameOverUI;
    public SceneFader fader;

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;


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
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        fader.FadeTo(nextLevel);
    }

}
