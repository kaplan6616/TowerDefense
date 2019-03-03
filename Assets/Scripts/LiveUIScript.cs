using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveUIScript : MonoBehaviour
{
    public Text liveText;
    // Update is called once per frame
    void Update()
    {
        liveText.text = "Remaining Lives " + playerStats.Lives;
    }
}
