using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class waveSpawner : MonoBehaviour {

    public Transform enemyPrefab;

    public float countDownTimerBetweenWaves = 5f;

    private float countDown = 2f; // ilk dalga 2 saniye içinde gelsin diye

    private int waveNumber=1;

    public Text waveCountDownText;
    void Update()
    {
        if(countDown<=0f)
        {
            StartCoroutine(SpawnWave());
            countDown = countDownTimerBetweenWaves;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}",countDown);
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        playerStats.Rounds++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

     void SpawnEnemy()
    {
        Instantiate(enemyPrefab, wayPoints.points[0].transform.position, Quaternion.identity);       
    }
}
