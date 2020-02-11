using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private TurretBluePrint turretToBuild;

    public GameObject standartTurretPrefab;
    public GameObject missileLauncerPrefab;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node) {
        if(PlayerStats.Money < turretToBuild.cost) {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.turretPrefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);
    }
    public void SelectTurretToBuild(TurretBluePrint turretToBuild) {
        this.turretToBuild = turretToBuild;
    }
}