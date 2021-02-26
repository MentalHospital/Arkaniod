using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public bool isFree = true;
    public event Action<Ball> OnBallDestroyed;
    [SerializeField] private Vector2 startVelocity = new Vector2(0f, 2f);
    [SerializeField] private float startSpeed;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private float velocityComponentMinimum = 0.3f;
    private const float paddleHalfLength = 2.5f;
    private const float flatPartWidthPercent = 0.7f;
    private Vector2 oldVelocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void FixedUpdate()
    {
        if (isFree)
        {
            rb.velocity = rb.velocity.normalized * startSpeed;
            // Doesn't let ball to move completely in one axis
            if (Mathf.Abs(rb.velocity.y) < velocityComponentMinimum)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y >= 0 ? -velocityComponentMinimum : velocityComponentMinimum);
            if (Mathf.Abs(rb.velocity.x) < velocityComponentMinimum)
                rb.velocity = new Vector2(rb.velocity.x >= 0 ? -velocityComponentMinimum : velocityComponentMinimum, rb.velocity.y);
            oldVelocity = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BallDestroyer"))
        {
            SoundManager.instance.Play(SoundManager.Sound.BallDestroyed);
            OnBallDestroyed?.Invoke(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var contact = other.GetContact(0);
        var otherObject = other.gameObject;

        if (otherObject.CompareTag("Paddle"))
        {
            Vector2 hitpointLocalPosition = contact.point - (Vector2)otherObject.GetComponent<Rigidbody2D>().position;
            float reflectionAngle = CalculatePaddleReflectionAngle(hitpointLocalPosition);
            rb.velocity = new Vector2(Mathf.Cos(reflectionAngle * Mathf.Deg2Rad), Mathf.Sin(reflectionAngle * Mathf.Deg2Rad)) * startSpeed;
            SoundManager.instance.Play(SoundManager.Sound.PaddleHit);
        }
        else 
        {
            rb.velocity = Vector2.Reflect(oldVelocity, contact.normal).normalized * startSpeed;
            if (otherObject.CompareTag("Brick"))
            {
                //возможно нужно будет перенести в brick
                var brick = otherObject.GetComponent<Brick>();
                SoundManager.instance.Play(brick.GetSound());
                brick.TakeDamage();
            }
            else
            {
                SoundManager.instance.Play(SoundManager.Sound.WallHit);
            }
        }
    }

    private float CalculatePaddleReflectionAngle(Vector2 pointLocalPosition)
    {
        float reflectionAngle;
        if (Mathf.Abs(pointLocalPosition.x) < flatPartWidthPercent * paddleHalfLength)
        {
            float angleDeviationFlat = 30f * pointLocalPosition.x / (paddleHalfLength * flatPartWidthPercent);
            reflectionAngle = 90f - angleDeviationFlat;
        }
        else
        {
            float angleDeviationCurved = 30f + 50f * (Mathf.Abs(pointLocalPosition.x) - flatPartWidthPercent * paddleHalfLength) / ((1 - flatPartWidthPercent) * paddleHalfLength);
            reflectionAngle = 90f - Mathf.Sign(pointLocalPosition.x) * angleDeviationCurved;
        }
        return reflectionAngle;
    }

    public void LockPosition()
    {
        isFree = false;
        rb.simulated = false;
    }

    public void LockTo(Transform tr)
    {
        LockPosition();
        transform.parent = tr;
    }

    public void Unlock()
    {
        transform.parent = null;
        isFree = true;
        rb.simulated = true;
    }

    public void MoveTo(Vector3 position)
    {
        rb.position = position;
        transform.position = position;
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }

    private void ResetBall()
    {
        rb.position = startPosition;
        rb.velocity = startVelocity;
        startSpeed = startVelocity.magnitude;
    }

    public void Respawn()
    {
        ResetBall();
    }
}