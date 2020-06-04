using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerInstructionsMountain : MonoBehaviour
{
    public static GameManagerInstructionsMountain Instance;

    public GameObject top; 
    public int speed;                                                           // speed to move the landscape
    public GameObject landscape;                                                 // landscape object
    private bool movement;
    public float delay; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        movement = false;
        StartCoroutine(StartMoving(delay));
    }

    private void Update()
    {
        // move to next level when movement is true
        if (movement) MoveLandscape();
    }


    /*
     * Move landscape to next level
     */
    private void MoveLandscape()
    {
        landscape.transform.Translate(Vector3.up * Time.deltaTime * speed);

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
