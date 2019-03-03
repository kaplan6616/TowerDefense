using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static int money;
    public int startMoney = 400;
    static GameMenager gm;
    public static int Lives;
    public int startLive = 20;
    public static int Rounds;
    // Start is called before the first frame update
    void Start()
    {
        Rounds = 0;
        money = startMoney;
        Lives = startLive;
        gm =GameObject.FindGameObjectWithTag("GM").GetComponent<GameMenager>();
    }
    public static void decreaseLives()
    {
        Lives--;
        if(Lives==0)
        {
            gm.EndGame();
        }
    }

}
