using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseUI;

    public SceneFader fader;

    public string menuSceneName = "MainMenu";

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if (GameManager.IsGameOver)
            return;

        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    public void Retry() {
        TogglePause();
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        TogglePause();
        fader.FadeTo(menuSceneName);
    }
}
