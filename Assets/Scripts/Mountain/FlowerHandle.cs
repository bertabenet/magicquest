﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerHandle : MonoBehaviour
{
    private bool goUp;
    public int speed;

    private void Start()
    {
        goUp = false;
    }

    private void Update()
    {
        if (goUp)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);

            if (gameObject.transform.localPosition.y > 98.0f)
                goUp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            goUp = true;
            GameManagerMountain.Instance.GameOver();
        }
    }

}