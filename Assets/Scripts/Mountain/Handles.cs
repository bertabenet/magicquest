using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handles : MonoBehaviour
{
    public Material activateColor;

    public Material deactivateColor;
    public int level;

    void Start()
    {
       // deactivateColor = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BodyPart"))
        {
            gameObject.GetComponent<Renderer>().material.color = activateColor.color;
            GameManagerMountain.Instance.IncreaseLevelCounter(level);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BodyPart"))
        {
            gameObject.GetComponent<Renderer>().material.color = deactivateColor.color;
            GameManagerMountain.Instance.DecreaseLevelCounter(level);
        }

    }
}
