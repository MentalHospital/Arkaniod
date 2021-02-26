using UnityEngine;

public class Grabber : PaddleUpgrade
{
    public bool isInfinite = false;
    // сделать чтобы при старте это поле выставлялось автоматически
    [SerializeField] private bool isAvailable = false;
    private PaddleUpgradeManager paddleUpgradeManager;
    private Ball ball;

    private void Start()
    {
        paddleUpgradeManager = FindObjectOfType<PaddleUpgradeManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isAvailable && ball != null)
            {
                ball.Unlock();
                isAvailable = true;
                if (!isInfinite)
                {
                    // Можно переписать как ивент, на который будет подписываться paddleUpgradeManager при добавлении this в currentList,
                    // этот ивент можно переместить в parent класс.
                    paddleUpgradeManager.DestroyUpgrade(this);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isAvailable)
        {
            ball = collision.gameObject.GetComponent<Ball>();
            if (ball.isFree)
            {
                isAvailable = false;
                ball.LockTo(this.transform);
            }
        }
    }

    public void Grab(Ball ball)
    {
        this.ball = ball;
        ball.LockTo(this.transform);
        isAvailable = false;
    }
}
