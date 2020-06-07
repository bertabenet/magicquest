using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTitle : MonoBehaviour
{
    private bool arrivedFadeOutPosition;

    public Text title;
    public RawImage fadeOut;

    public float speed;
    public float increaseAlphaSpeed;
    public float fadeOutSpeed;
    public int fadeOutPoint;
    public int endPoint;

    // Start is called before the first frame update
    void Start()
    {
        arrivedFadeOutPosition = false;
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0.0f);
        fadeOut.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        /*
         * 1- TITLE
         * 2- Instructions Diamonds
         * 3- Diamonds
         * 4- Instructions Mountain
         * 5- Mountain
         * 6- Tornar a jugar / sortir
         */
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
            fadeOut.color += new Color(0, 0, 0, fadeOutSpeed);
            if (fadeOut.color.a >= 2)
                SceneManager.LoadScene("Instructions_Diamonds");
        }

    }
}
