using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorRenderer : MonoBehaviour
{
    //private Color grayColor;
    private Color normalColor;

    void Start()
    {
        normalColor = gameObject.GetComponent<MeshRenderer>().material.color;
        //grayColor = new Color(50, 200, 150);
    }

    public void InactiveColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
    }

    public void ActiveColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = normalColor;
    }
}
