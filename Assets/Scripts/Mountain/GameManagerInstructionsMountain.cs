using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerInstructionsMountain : MonoBehaviour
{
    public static GameManagerInstructionsMountain Instance;

    public GameObject top; 
    public int speed;               // speed to move the landscape
    public GameObject landscape;    // landscape object
    private bool movement;
    public float delay;
    public Text instructions;

    private bool fadeOut;           // start/stop fading out
    public RawImage fade;           // fade black screen
    public float fadeSpeed;         // fading speed

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeOut = false; 
        movement = false;
        fade.color = Color.black; 
        StartCoroutine(StartMoving(delay));
    }

    private void Update()
    {
        // move to next level when movement is true
        if (movement) MoveLandscape();

        // always fade in
        fade.color -= new Color(0, 0, 0, fadeSpeed);

        // fade instructions out at the end
        if (fadeOut)
            instructions.color -= new Color(0, 0, 0, fadeSpeed);
    }


    /*
     * Move landscape to next level
     */
    private void MoveLandscape()
    {
        landscape.transform.Translate(Vector3.up * Time.deltaTime * speed);

        if(landscape.transform.position.y > (top.transform.position.y - 50))
            fadeOut = true;

        if (landscape.transform.position.y > top.transform.position.y)
        {
            movement = false;
            SceneManager.LoadScene("Mountain");
        }
    }



    private IEnumerator StartMoving(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        movement = true;
    }
}
