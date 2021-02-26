using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class DestroyerGenerator : MonoBehaviour
{
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
        var pointsArray = new Vector2[2];
        pointsArray[0] = cameraMain.ViewportToWorldPoint(new Vector2(0, 0));
        pointsArray[1] = cameraMain.ViewportToWorldPoint(new Vector2(1, 0));
        edgeCollider.points = pointsArray;
    }
}
