using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRecalculator : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform spriteTransform;
    [SerializeField] Vector2 scale = Vector2.one;
    void OnValidate()
    {
        boxCollider.size = scale;
        spriteTransform.localScale = new Vector3(scale.x, scale.y, 1);
    }
}
