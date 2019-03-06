using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class nodeUIScript : MonoBehaviour
{
    private buildingOnNode target;
    public Button upgradeButton;
    public GameObject UI;
    public Vector3 offset;
    GameObject Turret;
    public Text upgradeCost;

    public void SetTarget(buildingOnNode _target)
    {
        target = _target;
        transform.position = target.transform.position + offset;
        UI.SetActive(true);
        if( target.getTurret().GetComponent<turretScript>().turretLevel==5)
        {
            upgradeCost.text = "UPGRADE COMPLETED";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
            string type = target.getTurret().GetComponent<turretScript>().type.ToString();
            Debug.Log(type);
            if (type == "Standard")
            {
                Debug.Log(target.standardTurretUpgrade.upgradeCost.ToString());
                upgradeCost.text = "UPGRADE\n$" + target.standardTurretUpgrade.upgradeCost.ToString();
            }
            else if (type == "Launcher")
            {
                upgradeCost.text = "UPGRADE\n$" + target.missileLauncherUpgrade.upgradeCost;
            }
            else if (type == "StandardV2")
            {
                upgradeCost.text = "UPGRADE\n$" + target.standardTurretV2Upgrade.upgradeCost;
            }
            else if (type == "LaserBeamer")
            {
                upgradeCost.text = "UPGRADE\n$" + target.laserBeamerUpgrade.upgradeCost;
            } 
        }
      
    }
    public void Hide()
    {
        UI.SetActive(false);
    }
    public void Sell()
    {
        playerStats.money += 100;
        target.setTurretToNull(target) ;
        Hide();
    }
    public void upgrade()
    {
        
        target.upgrade();
        buildMenager.child.deselectNode();
    }
}
