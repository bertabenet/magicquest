using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerInstructionsDiamonds : MonoBehaviour
{

    public float delay; 


    private void Start()
    {
        StartCoroutine(StartMoving(delay));
    }

    private IEnumerator StartMoving(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Diamonds");
    }
}
