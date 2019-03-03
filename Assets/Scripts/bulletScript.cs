using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    private Transform target;
    public int damageAmount = 50;
    public float speed = 70f;
    public float explosionRadius = 0f;

    public GameObject bulletHitPrefab;
    public void seek(Transform newTarget)
    {
        target = newTarget;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(direction.magnitude<=distance)
        {
            targetHit();
            return;
        }
        transform.Translate(direction.normalized * distance,Space.World);
        transform.LookAt(target);
	}
    void targetHit()
    {
       GameObject effect= Instantiate(bulletHitPrefab, transform.position, Quaternion.identity);

       if(explosionRadius>0)
       {
           Explode();
       }
       else
       {
           Damage(target);
       }
       Destroy(effect, 2f);

       
       Destroy(gameObject);
    }
    void Explode()
    {
        // missile için bulletin düştüğü yerden bir yarıçap dahilinde sphere çizilecek ve ordaki tüm colliderlerin bilgisi toplanacak
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in colliders)
        {
            if(col.tag=="enemy")
            {
                Damage(col.transform);
            }
        }
    }
    void Damage(Transform enemyObject)
    {
        enemy e = enemyObject.GetComponent<enemy>();
        if(e!=null)
        {
            e.takeDamage(damageAmount);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
