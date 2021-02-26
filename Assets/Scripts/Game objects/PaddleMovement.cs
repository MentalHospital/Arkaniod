using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private bool isControlled = true;
    private Rigidbody2D rb;
    private Camera cameraMain;
    private float initialY = 0;
    private float deltaX = 0.025f;
    private Vector3 mouseWorldPosition;
    private Vector2 rbTarget;

    private EdgeGenerator edgeGenerator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraMain = Camera.main;
        edgeGenerator = FindObjectOfType<EdgeGenerator>();
    }

    void Start()
    {
        initialY = rb.position.y;
    }

    void FixedUpdate()
    {
        if (!isControlled)
            return;

        mouseWorldPosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        rbTarget.x = Mathf.Clamp(mouseWorldPosition.x, edgeGenerator.BottomLeft.x, edgeGenerator.TopRight.x);
        rbTarget.y = initialY;
        rb.MovePosition(rbTarget);
    }

    public void LockPosition()
    {
        isControlled = false;
        rb.simulated = false;
    }

    public void UnlockPosition()
    {
        isControlled = true;
        rb.simulated = true;
    }
}
