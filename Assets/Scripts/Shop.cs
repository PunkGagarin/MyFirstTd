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
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab);
    }

    public void PurchaseMissileLauncer()
    {
        Debug.Log("Missile Launher Selected");
        buildManager.SetTurretToBuild(buildManager.missileLauncerPrefab);
    }
}
