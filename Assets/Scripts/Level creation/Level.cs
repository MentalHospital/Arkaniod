using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level
{
    private List<Vector3> bricksPositions;
    private List<int> bricksType;
    public int id;
    public float timeLimit;
}
