using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
    BuildManager buildManager;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;

    [HideInInspector]
    public bool isUpgraded = false;


    private Renderer rend;
    private Color startColor;


    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null) {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.getTurretToBuild());
    }

    void BuildTurret(TurretBluePrint bluePrint) {
        if (PlayerStats.Money < bluePrint.cost) {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= bluePrint.cost;
        GameObject turret = (GameObject)Instantiate(bluePrint.turretPrefab, GetBuildPosition(), Quaternion.identity);
        this.turret = turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);
    }

    public void UpgradeTurret() {
        if (PlayerStats.Money < turretBluePrint.upgradeCost) {
            Debug.Log("Not enough money to upgrade");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;

        //Get rid of the old turret
        Destroy(this.turret);
        GameObject turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        this.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded! Money left: " + PlayerStats.Money);
    }

    public void SellTurret() {
        PlayerStats.Money += turretBluePrint.GetSellAmount();


        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);


        Destroy(turret);
        turretBluePrint = null;
        isUpgraded = false;
    }

    void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //TODO: проверить
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }
}
