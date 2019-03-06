using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseUI;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
        {
            PauseOrContinue();
        }
    }

    public void PauseOrContinue()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if(pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
