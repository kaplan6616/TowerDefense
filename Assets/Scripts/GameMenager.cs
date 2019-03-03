using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool GameIsOver;
    void Start()
    {
        GameIsOver = false;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        Debug.Log("Game Has Ended");
        GameIsOver = true;
        gameOverUI.SetActive(true);
        
    }
}
