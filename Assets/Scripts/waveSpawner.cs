using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class waveSpawner : MonoBehaviour {

    [SerializeField]
    public static int alivingEnemyCount = 0;
    public int StartAlive;
    public WaveClass[] waves;
    public float countDownTimerBetweenWaves = 5f;

    private float countDown = 2f; // ilk dalga 2 saniye içinde gelsin diye

    private int waveNumber=0;

    public Text waveCountDownText;

    void Start()
    {
        waveNumber = 0;
        alivingEnemyCount = 0;
    }
    void Update()
    {
        StartAlive = alivingEnemyCount;
        if(alivingEnemyCount>0)
        {
            return;
        }
        if(countDown<=0f)
        {
            StartCoroutine(SpawnWave());
            countDown = countDownTimerBetweenWaves;
            return;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}",countDown);
    }

    IEnumerator SpawnWave()
    {
        if(waveNumber==waves.Length)
        {         
            this.enabled = false;
            if (PlayerPrefs.GetInt("LevelNo", 1) < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("LevelNo", SceneManager.GetActiveScene().buildIndex);
            }
            SceneManager.LoadScene(1); //levelSelector
        }
        else
        {
            
            Debug.Log(waveNumber);
            WaveClass wave = waves[waveNumber];
            alivingEnemyCount = wave.enemyCount;
            playerStats.Rounds++;
            for (int i = 0; i < wave.enemyCount; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1 / wave.spawnRate);
            }
            waveNumber++;
        }       
    }

     void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, wayPoints.points[0].transform.position, Quaternion.identity);
        
    }
}
