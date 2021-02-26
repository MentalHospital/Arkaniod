using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI livesTextMesh;

    private void Start()
    {
        UpdateScore(0);
    }
    public void UpdateScore(int score)
    {
        scoreTextMesh.text = score.ToString();
    }

    public void UpdateLiveCount(int liveCount)
    {
        livesTextMesh.text = liveCount.ToString();
    }
}
