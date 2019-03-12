using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour
{
    public Button[] levelButtons;
    public static LevelSelectorScript level;
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponentInChildren<Text>().text = (i+1).ToString();
        }
        for (int i = PlayerPrefs.GetInt("LevelNo",1); i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }
    public void LevelSelector(Text levelNo)
    {
        if(PlayerPrefs.GetInt("LevelNo",1)<int.Parse(levelNo.text))
        {
            PlayerPrefs.SetInt("LevelNo", int.Parse(levelNo.text));
        }
        SceneManager.LoadScene(int.Parse(levelNo.text)+1);
    }
}
