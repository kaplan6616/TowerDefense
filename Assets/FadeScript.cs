using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    public Image image;
    public AnimationCurve curve;
    public Canvas canvas;
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    
    public void FadeToOut(string SceneName)
    {
        StartCoroutine(FadeOut(SceneName));
    }
    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t>0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;// skip to next frame
        }
        canvas.enabled = false;
    }
    IEnumerator FadeOut(string sceneName)
    {
        canvas.enabled = true;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;// skip to next frame
        }
        SceneManager.LoadScene(sceneName);
    }
    
}
