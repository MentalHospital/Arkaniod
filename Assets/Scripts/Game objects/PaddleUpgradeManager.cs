using System.Collections.Generic;
using UnityEngine;

public class PaddleUpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject paddle;
    public List<PaddleUpgrade> currentUpgrades;
    public List<PaddleUpgrade> possibleUpgrades;
    private void Awake()
    {
        currentUpgrades = new List<PaddleUpgrade>();
        AddUpgrade<Grabber>().isInfinite = false;
        //possibleUpgrades = new List<PaddleUpgrade>();
    }

    void Start()
    {
    }

    public void DestroyAllUpgrades()
    {
        foreach(var upgrade in currentUpgrades)
        {
            Destroy(upgrade);
        }

        currentUpgrades.Clear();
    }

    public void DestroyUpgrade(PaddleUpgrade upgrade)
    {
        if (currentUpgrades.Contains(upgrade))
        {
            Destroy(upgrade);
        }
    }

    private T AddUpgrade<T>() where T : PaddleUpgrade
    {
        var upgrade = paddle.AddComponent<T>();
        currentUpgrades.Add(upgrade);
        return upgrade;
    }
}
