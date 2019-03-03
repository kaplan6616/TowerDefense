using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marketScript : MonoBehaviour
{
   public GunsClass standardTurret;
   public GunsClass standardTurretV2;
   public  GunsClass missileLauncher;
   public GunsClass laserBeamer;
    buildMenager bm;
    void Start()
    {
        bm = buildMenager.child;
    }
    public void buyStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        bm.SetTurretToBuild(standardTurret);
    }
    public void buyStandardTurretV2()
    {
        Debug.Log("Standard Turret V2 Selected");
        bm.SetTurretToBuild(standardTurretV2);
    }
    public void buyMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        bm.SetTurretToBuild(missileLauncher);
    }
    public void buyLaserBeamer()
    {
        Debug.Log("Missile Launcher Selected");
        bm.SetTurretToBuild(laserBeamer);
    }
}
