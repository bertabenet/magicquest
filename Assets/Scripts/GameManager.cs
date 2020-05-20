using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public int diamondsCollected;
    [HideInInspector]
    public int livesLeft;

    public Spawner spawner;

    public int hitsBeforeGameOver;
    public int diamondsToWin;
    // public Text gameOver;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //gameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void collectDiamonds()
    {
        diamondsCollected += 1;
        UIManager.Instance.UpdateDiamondsCollected();
    }

    public void lessLife()
    {
        livesLeft += 1;
        UIManager.Instance.UpdateHitsTaken();
        if (livesLeft == hitsBeforeGameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        spawner.canSpawn = false;
        spawner.DestroyAllObjects();
        //gameOver.text = "GAME OVER";

    }
}