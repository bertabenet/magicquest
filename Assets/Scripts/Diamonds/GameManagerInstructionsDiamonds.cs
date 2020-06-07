using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerInstructionsDiamonds : MonoBehaviour
{
    private bool fadeIn;
    private bool fadeOut;

    public float delay;
    public float fadeSpeed;
    public RawImage fade;

    private void Start()
    {
        fade.color = Color.black;
        fadeIn = true;
        fadeOut = false;

        StartCoroutine(FadeOut(delay - 0.8f));
        StartCoroutine(StartMoving(delay));
    }

    private void Update()
    {
        if (fadeIn)
            fade.color -= new Color(0, 0, 0, fadeSpeed);

        if (fadeOut)
            fade.color += new Color(0, 0, 0, fadeSpeed);

        if (fade.color.a <= 0.0f)
            fadeIn = false;

    }

    private IEnumerator StartMoving(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Diamonds");
    }

    private IEnumerator FadeOut(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        fadeOut = true;
    }
}
