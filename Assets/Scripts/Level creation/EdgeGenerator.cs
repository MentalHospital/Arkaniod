using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeGenerator : MonoBehaviour
{
    public Vector3 BottomLeft { get; private set; }
    public Vector3 TopRight { get; private set; }

    private EdgeCollider2D edgeCollider;
    private Camera cameraMain;
    void Start()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    void Initialize()
    {
        cameraMain = Camera.main;
        transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y);
        edgeCollider = GetComponent<EdgeCollider2D>();
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        var pointsArray = new Vector2[4];
        pointsArray[0] = cameraMain.ViewportToWorldPoint(new Vector2(0, 0));
        pointsArray[1] = cameraMain.ViewportToWorldPoint(new Vector2(0, 1));
        pointsArray[2] = cameraMain.ViewportToWorldPoint(new Vector2(1, 1));
        pointsArray[3] = cameraMain.ViewportToWorldPoint(new Vector2(1, 0));

        //на будущее
        BottomLeft = pointsArray[0];
        TopRight = pointsArray[2];

        edgeCollider.points = pointsArray;
    }
}
