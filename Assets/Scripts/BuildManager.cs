using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private TurretBluePrint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public GameObject buildEffect;
    public GameObject sellEffect;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    //E18
    public void SelectNode(Node node) {

        if (selectedNode == node) {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode() {
        selectedNode = null;
        nodeUI.HideNodeUI();
    }

    public void SelectTurretToBuild(TurretBluePrint turretToBuild) {
        this.turretToBuild = turretToBuild;
        DeselectNode();
    }

    public TurretBluePrint getTurretToBuild() {
        return turretToBuild;
    }
}