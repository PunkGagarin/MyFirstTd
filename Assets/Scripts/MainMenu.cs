using System;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public SceneFader fader;

    public string levelToLoad = "MainLevel";

    public void Play() {
        fader.FadeTo(levelToLoad);
    }

    public void SelectLevelScene() {
        string levelSelect = "LevelSelect" + Enum.GetName(typeof(GameStats.Difficulty), GameStats.difficulty);
        fader.FadeTo(levelSelect);
    }

    public void Quit() {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    #region Difficulty select

    public void setEasy() {
        GameStats.difficulty = GameStats.Difficulty.Easy;
    }


    public void setNormal() {
        GameStats.difficulty = GameStats.Difficulty.Normal;
    }


    public void setHard() {
        GameStats.difficulty = GameStats.Difficulty.Hard;
    }

    #endregion
}
