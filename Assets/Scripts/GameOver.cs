using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

    public SceneFader fader;
    public string menuSceneName = "MainMenu";


    public void Retry() {
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        fader.FadeTo(menuSceneName);
    }
}
