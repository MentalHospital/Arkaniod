using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public static ScoreManager instance;
    private void Start()
    {
        Score = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }
}
