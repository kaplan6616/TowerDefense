using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour {
    [Header("Enemy Attributes") ]
    public float startSpeed = 10f;
    private float speed = 10;
    public float startHealth = 100;
    public float health;
    public int moneyGainFromEnemy=50;
    public GameObject deathEffect;

    [Header("Enemy AI")]
    private Transform targetPoint;
    private int currentPointIndex = 0;

    [Header("HealthBarStuff")]
    public Image healthBar;

	void Start () 
    {
        targetPoint = wayPoints.points[0];
        speed = startSpeed;
        health = startHealth;
	}
    public void Slow(float slowAmount)
    {
        speed = startSpeed * slowAmount;
    }
	void Update () 
    {
        Vector3 direction = targetPoint.position - transform.position;
        transform.Translate(direction.normalized*speed*Time.deltaTime,Space.World);
        float difference = Vector3.Distance(transform.position, targetPoint.transform.position);
        if(difference<0.5f)
        {
            currentPointIndex++;
            if(currentPointIndex==wayPoints.points.Length)
            {
                endPath();
                return;
            }
            targetPoint = wayPoints.points[currentPointIndex];
        }
        // turretScript slow fonksiyonunu çağırıp sürekli yavaşlatma uygular. Enemy range içinden çıktığında eski hızına döner.
        // turretScript calls slow func all the time but if enemy will be out of turret range then it will have its own speed.
        // be sure the in scriptExecution settings turret script will execute before this script.
        speed = startSpeed; 
	}
    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health<=0)
        {
            Die();
            
        }
        healthBar.fillAmount = health / startHealth;
    }

    void Die()
    {
        playerStats.money += moneyGainFromEnemy;
        GameObject go = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(go, 5f);
        Destroy(gameObject);
        waveSpawner.alivingEnemyCount--;
    }
    void endPath()
    {
        playerStats.decreaseLives();
        waveSpawner.alivingEnemyCount--;
        Destroy(gameObject);
    }
}
