using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab);
    }

    public void PurchaseAnotherTurret()
    {
        Debug.Log("Another Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab);
    }
}
