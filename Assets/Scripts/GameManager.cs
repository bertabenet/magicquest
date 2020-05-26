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
    public int livesLost;

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

    public void collectDiamond()
    {
        diamondsCollected += 1;
        UIManager.Instance.UpdateDiamondsCollected(true);
        if (diamondsCollected == diamondsToWin)
        {
            GameOver();
        }
    }

    public void lessLife()
    {
        livesLost += 1;
        UIManager.Instance.UpdateHitsTaken();
        if (livesLost == hitsBeforeGameOver)
        {
            GameOver();
        }
    }

    public void loseDiamond()
    {
        if (diamondsCollected > 0)
            diamondsCollected -= 1;
        UIManager.Instance.UpdateDiamondsCollected(false);
    }

    private void GameOver()
    {
        spawner.canSpawn = false;
        spawner.DestroyAllObjects();
        //gameOver.text = "GAME OVER";

    }
}