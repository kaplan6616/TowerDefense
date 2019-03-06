using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class buildingOnNode : MonoBehaviour {
    turretScript tp;
    public static buildingOnNode building;
    public Color hoverColor;
    private Color startColor;  
    private Renderer rd;
    public GameObject buildEffect;
    buildMenager bm;

    [HideInInspector]
    public GameObject turret;
    public GunsClass standardTurretUpgrade;
    public GunsClass standardTurretV2Upgrade;
    public GunsClass missileLauncherUpgrade;
    public GunsClass laserBeamerUpgrade;
    [HideInInspector]
    public bool isUpgraded=false;

    public Text upgradeCost;
    public Vector3 positionOffset;
      void Awake()
    {
        
        rd = GetComponent<Renderer>();
        startColor = rd.material.color;
    }
    void Start()
    {
        bm = buildMenager.child;
    }
  
	void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject()) // satın alma menüsü resimleri nodeların üzerine denk geldiğinde nodea tıklama yapılmayacak
        {
            return;
        }
        if (bm.GetTurretToBuild() == null)
        {
            return;
        }
        if(bm.hasMoney)
        {
            rd.material.color = hoverColor;
        }
        else
        {
            rd.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        rd.material.color = startColor;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // satın alma menüsü resimleri nodeların üzerine denk geldiğinde nodea tıklama yapılmayacak
        {
            return;
        }
        if(turret!=null)
        {
            bm.selectNode(this);
            return;
        }
        if (bm.GetTurretToBuild() == null)
        {
            return;
        }
        else
        {
            build();
        }
    }
    public void build()
    {
        
            GunsClass turretToBuild = bm.GetTurretToBuild();
            if (turretToBuild.cost > playerStats.money)
            {
                Debug.Log("Not Enough Money");
                return;
            }
            else
            {
                turret = Instantiate(turretToBuild.prefab, transform.position + positionOffset, transform.rotation);
                GameObject effect = Instantiate(buildEffect, transform.position + positionOffset, Quaternion.identity);
                Destroy(effect, 5f);
                playerStats.money -= turretToBuild.cost;
            }                                       
    }
    public void upgrade()
    {
        turretScript _turret = turret.GetComponent<turretScript>();
        if (_turret.type.ToString() == "StandardV2")
        {
          //  upgradeCost.text = "$" + standardTurretV2Upgrade.upgradeCost.ToString();
            if (_turret.isUpgraded == true)
            {
                //TODO:buton inaktif olsun
            }
            if (standardTurretV2Upgrade.upgradeCost > playerStats.money)
            {
                Debug.Log("Not Enough Money");
                return;
            }
            _turret.turretLevel++;
            _turret.range++;
            if (_turret.turretLevel == 5)
            {
                _turret.isUpgraded = true;
                Destroy(turret);
                turret = Instantiate(standardTurretV2Upgrade.upgradedPrefab, getBuildPosition(), transform.rotation);
                turret.GetComponent<turretScript>().turretLevel = 5;
            }
            playerStats.money -= standardTurretV2Upgrade.upgradeCost;
            standardTurretV2Upgrade.upgradeCost += 30;
        }
        else if (_turret.type.ToString() == "Standard")
        {
          //  upgradeCost.text = "$" + standardTurretUpgrade.upgradeCost.ToString();
            if (_turret.isUpgraded == true)
            {
                //TODO:buton inaktif olsun
            }
            if (standardTurretUpgrade.upgradeCost > playerStats.money)
            {
                Debug.Log("Not Enough Money");
                return;
            }
            _turret.turretLevel++;
            _turret.range++;
            if (_turret.turretLevel == 5)
            {
                Destroy(turret);
                turret = Instantiate(standardTurretUpgrade.upgradedPrefab, getBuildPosition(), transform.rotation);
                turret.GetComponent<turretScript>().turretLevel = 5;
                _turret.isUpgraded = true;
            }
            playerStats.money -= standardTurretUpgrade.upgradeCost;
            standardTurretUpgrade.upgradeCost += 30;
        }
        else if (_turret.type.ToString() == "Launcher")
        {
            
            if (_turret.isUpgraded == true)
            {
                //TODO:buton inaktif olsun
            }
            if (missileLauncherUpgrade.upgradeCost > playerStats.money)
            {
                Debug.Log("Not Enough Money");
                return;
            }
            if (_turret.turretLevel == 5)
            {
                
                Destroy(turret);
                turret = Instantiate(missileLauncherUpgrade.upgradedPrefab, getBuildPosition(), transform.rotation);
                turret.GetComponent<turretScript>().turretLevel = 5;
                _turret.isUpgraded = true;
            }
            _turret.turretLevel++;
            _turret.range++;
            playerStats.money -= missileLauncherUpgrade.upgradeCost;
            missileLauncherUpgrade.upgradeCost += 90;
        }
        else if (_turret.type.ToString() == "LaserBeamer")
        {
           // upgradeCost.text = "$" + laserBeamerUpgrade.upgradeCost.ToString();
            if (_turret.isUpgraded == true)
            {
                //TODO:buton inaktif olsun
            }
            if (laserBeamerUpgrade.upgradeCost > playerStats.money)
            {
                Debug.Log("Not Enough Money");
                return;
            }
            _turret.turretLevel++;
            _turret.range++;
            if (_turret.turretLevel == 5)
            {
                Destroy(turret);
                turret = Instantiate(laserBeamerUpgrade.upgradedPrefab, getBuildPosition(), transform.rotation);
                turret.GetComponent<turretScript>().turretLevel = 5;
                _turret.isUpgraded = true;
            }
            playerStats.money -= laserBeamerUpgrade.upgradeCost;
            laserBeamerUpgrade.upgradeCost += 85;

        }
    }
    public void setTurretToNull(buildingOnNode node)
    {
        Destroy(node.turret);
        node.turret = null;
    }
    public GameObject getTurret()
    {
        return turret;
    }
    Vector3 getBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
