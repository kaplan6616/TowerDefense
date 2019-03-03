using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class buildingOnNode : MonoBehaviour {

    public Color hoverColor;
    private Color startColor;  
    private Renderer rd;
    public GameObject buildEffect;
    buildMenager bm;
    private GameObject turret;

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
        if(bm.GetTurretToBuild()==null)
        {
            return;
        }
        else
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
                GameObject effect=  Instantiate(buildEffect, transform.position + positionOffset, Quaternion.identity);
                Destroy(effect, 5f);
                playerStats.money -= turretToBuild.cost;
            }
            
        }
    }
}
