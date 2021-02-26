using TMPro;
using UnityEngine;
using System.Collections;

public class StatsEndLevel : MonoBehaviour
{
    [SerializeField] private GameObject endLevelStatsPanel;
    [SerializeField] private TextMeshProUGUI playTimeMesh;
    [SerializeField] private TextMeshProUGUI ballsLeftMesh;
    [SerializeField] private TextMeshProUGUI finalScoreMesh;
    //[SerializeField] private TextMeshProUGUI ballsBonusText;

    private GameStateController gameStateController;
    
    void Start()
    {
        gameStateController = FindObjectOfType<GameStateController>();
        gameStateController.OnFinishLevel += ShowFinalStats;
        endLevelStatsPanel.SetActive(false);
    }

    private void ShowFinalStats()
    {
        StartCoroutine(ShowFinalStatsRoutine());
    }

    private IEnumerator ShowFinalStatsRoutine()
    {
        yield return new WaitForSeconds(2f);
        endLevelStatsPanel.SetActive(true);
        UpdateStats();
    }

    private void UpdateStats()
    {
        playTimeMesh.text = Mathf.RoundToInt(gameStateController.PlayTime).ToString();
        ballsLeftMesh.text = gameStateController.LivesCount.ToString();
        finalScoreMesh.text = (gameStateController.BallsOnScreen * 100).ToString();
    }
}
