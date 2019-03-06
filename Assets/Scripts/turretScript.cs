using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour {
    [System.Serializable]
    public enum typeOfTurret
    {
        Standard,
        StandardV2,
        Launcher,
        LaserBeamer
    }
  //  public GunsClass _turretToUpgrade;
    public typeOfTurret type;
    public int turretLevel;
    buildingOnNode build;
    private Transform target;
    private enemy targetEnemy;
    [Header("SetupObjects")]
    public Transform rotationPart;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("UsingLaser")]
    public bool useLaser = false;
    public float damageOverTime = 30f;
    public float slowAmount = 0.5f;
    public LineRenderer lr;
    public ParticleSystem laserEffect;
    public Light glowLight;

    [Header("TurretSpecifications")]
    public float range = 10f;
    public float turnSpeed = 10;
    public float fireRate = 1f;
    private float fireCountDown = 0;

    public bool isUpgraded=false;
	void Start () 
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
       // turretLevel = 1;
        build = GameObject.FindGameObjectWithTag("GM").GetComponent<buildingOnNode>();
	}
	
    void updateTarget()
    {
        if(target==null || Vector3.Distance(transform.position,target.position) > range) // en yakındaki target range içinde olduğu sürece ona vuracak.
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject shortestTarget = null;
            foreach (GameObject newTarget in targets)
            {
                float distance = Vector3.Distance(transform.position, newTarget.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestTarget = newTarget;
                }
            }
            if (shortestTarget != null && shortestDistance <= range) // en yakın hedef bulundu ve turretin range içindeyse
            {
                target = shortestTarget.transform;
                targetEnemy = shortestTarget.GetComponent<enemy>();
            }
            else
            {
                target = null;
            }
        }                      
    } 
	void Update () 
    {
		if(target==null)
        {
            if(useLaser)
            {
                if(lr.enabled)
                {
                    lr.enabled = false;
                    laserEffect.Stop();
                    glowLight.enabled = false;
                }
            }
            return;
        }       
            LockOnTheTarget();       
        

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }        
	}
    void LockOnTheTarget() 
    {
        Vector3 direction = target.position - transform.position;
        Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed).eulerAngles;
        rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Laser() // shooting laser
    {
        targetEnemy.takeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lr.enabled)
        {
            lr.enabled = true;
            laserEffect.Play();
            glowLight.enabled = true;
        }
            
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, target.position);
        Vector3 dir = lr.GetPosition(0) - lr.GetPosition(1);

        laserEffect.transform.position = target.position + dir.normalized ;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
    void shoot() //shooting bullet
    {
       GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       bulletScript bulletsc = bullet.GetComponent<bulletScript>();
       bulletsc.seek(target);
    }
    
    void OnDrawGizmosSelected() // unityde turret seçildiğinde onun pozisyonunda bir sphere çizecek bu onun target arayacağı kısım
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
