using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public SceneFader fader;

    public string levelToLoad = "MainLevel";

    public void Play() {
        fader.FadeTo(levelToLoad);
    }

    public void Quit() {
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
