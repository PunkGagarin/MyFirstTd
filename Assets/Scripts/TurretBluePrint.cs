using UnityEngine;

[System.Serializable]
public class TurretBluePrint {

    public GameObject turretPrefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount() {
        return cost / 2;
    }
}
