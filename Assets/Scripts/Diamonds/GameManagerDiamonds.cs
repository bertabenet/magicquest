using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerDiamonds : MonoBehaviour
{
    public static GameManagerDiamonds Instance;

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
        UIManagerDiamonds.Instance.UpdateDiamondsCollected(true);
        AudioManagerDiamonds.Instance.PlayPickDiamondClip();
        if (diamondsCollected == diamondsToWin)
        {
            UIManagerDiamonds.Instance.YouWin();
            GameOver(true);
        }
    }

    public void lessLife()
    {
        livesLost += 1;
        UIManagerDiamonds.Instance.UpdateHitsTaken();
        AudioManagerDiamonds.Instance.PlayHitByRockClip();
        if (livesLost == hitsBeforeGameOver)
        {
            UIManagerDiamonds.Instance.YouLose();
            GameOver(false);
        }
    }

    public void loseDiamond()
    {
        if (diamondsCollected > 0)
        {
            diamondsCollected -= 1;
        }
        AudioManagerDiamonds.Instance.PlayLoseDiamondClip();
        UIManagerDiamonds.Instance.UpdateDiamondsCollected(false);
    }

    private void GameOver(bool win)
    {
        spawner.canSpawn = false;
        spawner.DestroyAllObjects();

        string scene;
        if (win) scene = "Instructions_Mountain";
        else scene = "Instructions_Diamonds";

        StartCoroutine(ChangeSceneRoutine(scene, 4.0f));

    }

    private IEnumerator ChangeSceneRoutine(string scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(scene);
        
    }
}