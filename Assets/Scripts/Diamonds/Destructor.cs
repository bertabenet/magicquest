using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public string destructorTag;

    private void OnTriggerEnter(Collider other) // 1
    {
        if (other.CompareTag(destructorTag)) // 2
        {
            Destroy(gameObject); // 3
        }
    }
}