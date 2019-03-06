using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMenager : MonoBehaviour {
    public static buildMenager child;
    private buildingOnNode selectedNode;
    private GunsClass turretToBuild;
    private GameObject turretToUpgrade;
    public nodeUIScript nodeUI;
    public GunsClass GetTurretToBuild()
    {
        return turretToBuild;
    }
    public GameObject GetTurretToUpgrade()
    {
        return turretToUpgrade;
    }

    void Awake()
    {
        child = this;
    }

    public void SetTurretToBuild(GunsClass turret)
    {
        turretToBuild = turret;
        deselectNode();
    }
    public void SetTurretToUpgrade(turretScript turret)
    {
        turretToUpgrade = turret.gameObject;
        deselectNode();
    }
    public  void selectNode(buildingOnNode node)
    {
        if(selectedNode==node)
        {
            deselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        turretToUpgrade = node.getTurret();
        nodeUI.SetTarget(node);     
    }
    public void deselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public buildingOnNode getNode()
    {
        return selectedNode;
    }
    public bool hasMoney { get { return playerStats.money >= turretToBuild.cost; } } // node scriptinde para yetersizse node üzerine gelindiğinde renk kırmızı olsun diye.
}
