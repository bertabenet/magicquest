using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}
