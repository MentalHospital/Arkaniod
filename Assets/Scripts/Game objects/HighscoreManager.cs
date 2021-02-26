using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighscoreManager : MonoBehaviour
{
    private Dictionary<int, int> highscores;
    private string savePath;
    private GameStateController gameStateController;

    private static bool initialized = false;

    private void Awake()
    {
        if (initialized)
        {
            Destroy(this.gameObject);
        }
        else
        {
            initialized = true;
            DontDestroyOnLoad(this.gameObject);
        }

        savePath = Application.persistentDataPath + "/saves/savefile.sav";
        highscores = new Dictionary<int, int>();
        LoadHighscore();
        foreach (var key in highscores.Keys)
        {
            print(highscores[key]);
        }
    }

    private void Start()
    {
        //дичь
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) =>
        {
            gameStateController = FindObjectOfType<GameStateController>();
            Debug.Log(gameStateController);
            if (gameStateController != null)
                gameStateController.OnFinishLevel += SaveCurrentHighscore;
        };
    }

    private void LoadHighscore()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            highscores = (Dictionary<int, int>)formatter.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogError("File not found");
        }
    }

    private void SaveCurrentHighscore()
    {
        Level currentLevel = gameStateController.CurrentLevel;
        int currentScore = gameStateController.CurrentScore;
        Debug.Log("CURHS = " + currentScore);
        if (highscores[currentLevel.id] < currentScore)
        {
            highscores[currentLevel.id] = currentScore;
            SaveHighscores();
        }
    }
    private void SaveHighscores()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        formatter.Serialize(file, highscores);
        file.Close();
    }
}
