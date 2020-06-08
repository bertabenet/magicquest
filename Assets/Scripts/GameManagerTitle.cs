using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTitle : MonoBehaviour
{
    private bool arrivedFadeOutPosition;
    private bool fadeIn;

    public Text title;
    public RawImage fade;

    public float speed;
    public float increaseAlphaSpeed;
    public float fadeOutSpeed;
    public int fadeOutPoint;
    public int endPoint;

    // Start is called before the first frame update
    void Start()
    {
        arrivedFadeOutPosition = false;
        fadeIn = true;
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0.0f);
        fade.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

        title.color += new Color(0, 0, 0, increaseAlphaSpeed);
        Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        if (Camera.main.transform.position.z > fadeOutPoint)
        {
            arrivedFadeOutPosition = true;

        }

        if (arrivedFadeOutPosition)
        {
            fade.color += new Color(0, 0, 0, fadeOutSpeed);
            if (fade.color.a >= 2)
                SceneManager.LoadScene("Instructions_Diamonds");
        }

        if (fadeIn)
            fade.color -= new Color(0, 0, 0, fadeOutSpeed);

        if (fade.color.a <= 0.0f)
            fadeIn = false;

    }
}
