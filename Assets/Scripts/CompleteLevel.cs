using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

    public SceneFader fader;
    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;


    public void Menu() {
        fader.FadeTo(menuSceneName);
    }
    public void Continue() {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        fader.FadeTo(nextLevel);
    }

}
