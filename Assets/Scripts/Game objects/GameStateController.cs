using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameStateController : MonoBehaviour
{
    public event Action OnFinishLevel;
    public float PlayTime { get; private set; }
    public int LivesCount { get; private set; }
    public int BallsOnScreen { get; private set; }
    public Level CurrentLevel { get; private set; }
    public int CurrentScore { get; private set; }

    private int bricksCount;
    private StatsDisplay statsDisplay;
    private ScoreManager scoreManager;
    private UIManager uiManager;

    private float ballRadius = 1f;
    private float paddleHeight = 0.7f;
    private float gapHeight = 0.05f;


    private void Start()
    {
        var mapMaker = FindObjectOfType<MapMaker>();
        statsDisplay = FindObjectOfType<StatsDisplay>();
        scoreManager = FindObjectOfType<ScoreManager>();
        uiManager = FindObjectOfType<UIManager>();

        mapMaker.Initialize();
        bricksCount = mapMaker.BricksCount;
        LivesCount = 3;
        BallsOnScreen = 1;
        foreach (var brick in Brick.bricksList)
        {
            brick.onBrickBroken += OnBrickBrokenHandler;
        }
        var ball = FindObjectOfType<Ball>();
        ball.OnBallDestroyed += HandleBallDestruction;
        var paddle = FindObjectOfType<PaddleMovement>();
        ball.MoveTo(paddle.transform.position + Vector3.up * 0.5f * (ballRadius + paddleHeight + gapHeight));
        var grabber = FindObjectOfType<Grabber>();
        grabber.Grab(ball);
        OnFinishLevel += ball.LockPosition;

        //нехорошо
        CurrentLevel = new Level();
        CurrentLevel.timeLimit = 100f;
        CurrentLevel.id = SceneLoader.instance.GetSceneIndex();
        //OnFinishLevel += FindObjectOfType<PaddleMovement>().LockPosition;
    }

    private void OnBrickBrokenHandler(int score)
    {
        DecrementBricksCount(); 
        AddPointsForBrick(score);
    }

    private void DecrementBricksCount()
    {
        bricksCount -= 1;
        if (bricksCount <= 0)
        {
            WinLevel();
        }
    }

    private void HandleBallDestruction(Ball ball)
    {
        LivesCount -= 1;
        BallsOnScreen -= 1;
        statsDisplay.UpdateLiveCount(LivesCount);
        if (BallsOnScreen <= 0)
        {
            if (LivesCount <= 0)
            {
                LoseLevel();
            }
            else
            {
                RespawnBall(ball);
            }
        }
    }

    private void RespawnBall(Ball ball)
    {
        ball.Respawn();
        BallsOnScreen += 1;
    }

    private void AddPointsForBrick(int score)
    {
        scoreManager.AddScore(score);
        statsDisplay.UpdateScore(scoreManager.Score);
    }

    private void LoseLevel()
    {
        Debug.Log("YOU LOSE");
        FinishLevel();
    }

    private void WinLevel()
    {
        Debug.Log("YOU WIN");
        FinishLevel();
        //Unlock next level;
    }
    private void FinishLevel()
    {
        UpdateCurrentScore();
        uiManager.HideLevel();
        OnFinishLevel?.Invoke();
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            ball.Explode();
        }
    }

    private void UpdateCurrentScore()
    {
        CurrentScore = (int)(
               BallsOnScreen * 100
               + Mathf.Clamp(CurrentLevel.timeLimit - PlayTime, 0, CurrentLevel.timeLimit)
               );
    }

    //private void SaveHighscore(int levelID, int highscore)
    //{

    //}

    //private int[] ReadHighscore()
    //{
    //    string[] data = File.ReadAllLines(Application.persistentDataPath + "/player_data.spd");
    //    int[] result = new int[count]
    //}
}
