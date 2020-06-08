using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    public float fadingSpeed;
    public bool end;

    public bool handR;
    public bool handL;
    public bool head;

    private Color initColor;

    // Start is called before the first frame update
    void Start()
    {
        handR = false;
        handL = false;
        head = false;

        initColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(handR && handL && head)
            gameObject.GetComponent<Renderer>().material.color += new Color(0, 0, 0, fadingSpeed);
        else
            gameObject.GetComponent<Renderer>().material.color = initColor;

        if (gameObject.GetComponent<Renderer>().material.color.a >= 1.0f)
        {
            if (!end)
                SceneManager.LoadScene("Title");
            else
                Application.Quit();
        }
            

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandR"))
            handR = true;

        if (other.CompareTag("HandL"))
            handL = true;

        if (other.CompareTag("Head"))
            head = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandR"))
            handR = false;

        if (other.CompareTag("HandL"))
            handL = false;

        if (other.CompareTag("Head"))
            head = false;
    }
}
