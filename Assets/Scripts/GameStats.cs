using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    
    public enum Difficulty { Easy, Normal, Hard }

    public static Difficulty difficulty = PlayerPrefs.HasKey("difficulty") ? (Difficulty)PlayerPrefs.GetInt("difficulty") : (Difficulty) 0;

    public static int currentLevel = PlayerPrefs.HasKey("levelReached") ? PlayerPrefs.GetInt("levelReached") : 1;

    public static void setDifficulty(Difficulty difficulty) {
        PlayerPrefs.SetInt("difficulty", (int)difficulty);
    }
}
