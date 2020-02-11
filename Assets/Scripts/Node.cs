﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
    BuildManager buildManager;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

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

        if (!buildManager.CanBuild)
            return;

        if (turret != null) {
            Debug.Log("Can't build here! - TODO: Display on screen");
            return;
        }

        buildManager.BuildTurretOn(this);
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
