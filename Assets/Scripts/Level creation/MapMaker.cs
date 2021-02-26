using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public int BricksCount { get; private set; }

    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 4;
    [SerializeField] private float spacing = 0.1f;
    private float brickWidth = 2;
    private float brickHeight = 1;

    public void Initialize()
    {
        float fieldWidth = brickWidth * width + spacing * (width - 1);
        float fieldHeight = brickHeight * height + spacing * (height - 1);
        Vector3 centerPosition = new Vector3(fieldWidth / 2, fieldHeight / 2, 0);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 localPosition = new Vector3(x * brickWidth + x * spacing + brickWidth / 2, y * brickHeight + y * spacing + brickHeight / 2, 0) - centerPosition;
                var currentBrick = Instantiate(brickPrefab, transform.position + localPosition, Quaternion.identity);
                currentBrick.GetComponent<Brick>().Initialize();
                BricksCount++;
            }
        }
    }
}
