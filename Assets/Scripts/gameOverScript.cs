using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public Text rounds;
   void OnEnable()
    {
        rounds.text = playerStats.Rounds.ToString();
    }
    public void Restart()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
    public void Menu()
    {
        Debug.Log("Return to menu");
    }
}
