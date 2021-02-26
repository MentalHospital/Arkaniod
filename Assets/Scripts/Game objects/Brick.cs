using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour, IHasSound
{
    public static List<Brick> bricksList = new List<Brick>();

    public event System.Action<int> onBrickBroken;
    [SerializeField] private int score = 1;
    [SerializeField] private int health = 1;
    Color color = Color.white;

    public void Initialize()
    {
        if (bricksList == null)
        {
            bricksList = new List<Brick>();
        }
        bricksList.Add(this);

        health = Random.value > 0.3f ? 1 : 2;
        score = health;
        UpdateColor();
        
    }

    public void UpdateColor()
    {
        if (health > 1)
        {
            color = Color.gray;
        }
        else
        {
            color = Color.white;
        }
        GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public void TakeDamage()
    {
        health -= 1;
        UpdateColor();
        if (health <= 0)
        {
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        onBrickBroken?.Invoke(score);
        onBrickBroken -= ScoreManager.instance.AddScore;
        Destroy(this.gameObject);
    }

    public SoundManager.Sound GetSound()
    {
        if (health <= 1)
        {
            return SoundManager.Sound.BrickDestroyed;
        }
        else
        {
            return SoundManager.Sound.BrickHit;
        }
    }
}
