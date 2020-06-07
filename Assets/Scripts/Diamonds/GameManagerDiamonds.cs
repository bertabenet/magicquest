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

    public RawImage fade;
    public float fadeSpeed;
    private bool fadeIn;
    private bool fadeOut;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fade.color = Color.black;
        fadeIn = true;
        fadeOut = false;
        StartCoroutine(StopFadeIn(3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
            fade.color -= new Color(0, 0, 0, fadeSpeed);

        if (fadeOut)
            fade.color += new Color(0, 0, 0, fadeSpeed);
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

        //fadeIn = false;
        fadeOut = true;
        StartCoroutine(ChangeSceneRoutine(scene, 3.0f));

    }

    private IEnumerator ChangeSceneRoutine(string scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(scene);
        
    }

    private IEnumerator StopFadeIn(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        fadeIn = false;
    }

}