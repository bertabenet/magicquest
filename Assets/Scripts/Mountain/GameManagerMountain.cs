using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerMountain : MonoBehaviour
{
    public static GameManagerMountain Instance;
   
    public List<GameObject> level0 = new List<GameObject>();                    // rocks in level 0
    public List<GameObject> level1 = new List<GameObject>();                    // rocks in level 1
    public List<GameObject> level2 = new List<GameObject>();                    // rocks in level 2

    private int general_counter;                                                // increase once a level is complete
    private List<List<GameObject>> levels = new List<List<GameObject>>();       // keep track of all the levels' gameobjects
    private List<int> level_counter = new List<int>();                          // keep track of correct handles in each level

    public List<GameObject> centers = new List<GameObject>();                   // center for each level on the landscape
    public int speed;                                                           // speed to move the landscape
    public GameObject landscape;                                                // landscape object
    private bool movement;                                                      // move landscape

    public GameObject winText;
    public RawImage fade;
    private Color textColor;
    private bool fadeIn;
    private bool fadeOut;
    public float fadeTextSpeed;
    public float fadeOutSpeed;

    public Text timerText;
    public float minutesLeft;

    private bool restart;
    public int fallingSpeed;

    public GameObject gameOverText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        textColor = winText.GetComponent<Text>().color;
        fadeIn = false;
        fadeOut = false;
        minutesLeft *= 60;
        gameOverText.SetActive(false);

        // Add each level lists to general list "levels"
        levels.Add(level0);
        levels.Add(level1);
        levels.Add(level2);

        // Init level_counter to 0
        for (int i = 0; i < levels.Count; i++)
        {
            level_counter.Add(0);
        }

        movement = false;

        winText.GetComponent<Text>().color = new Color(textColor.r, textColor.g, textColor.b, 0.0f);
        fade.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    private void Update()
    {
        // move to next level when movement is true
        if (movement) MoveLandscape();
        if (restart) FallDown();

        if (fadeIn)
            winText.GetComponent<Text>().color += new Color(0, 0, 0, fadeTextSpeed);

        if (fadeOut)
            fade.color += new Color(0, 0, 0, fadeOutSpeed);

        if (minutesLeft > 0.0 & !fadeOut)
        {
            minutesLeft -= Time.deltaTime;
            int min = Mathf.FloorToInt(minutesLeft / 60);
            int sec = Mathf.FloorToInt(minutesLeft % 60);

            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }

        if (minutesLeft <= 0.0f)
        {
            timerText.text = "00:00";
            gameOverText.SetActive(true);
            FallDown();
        }
            
    }


    /*
     * One whole level is complete
     */
    public void IncreaseGeneralCounter()
    {
        general_counter++;
    }

    /*
     * Increase counter of one specific level
     */
    public void IncreaseLevelCounter(int level)
    {
        level_counter[level]++;
        if (level_counter[level] == levels[level].Count)
        {
            SetAllActive(level);
            IncreaseGeneralCounter();
            StartNextLevel(general_counter);
        }
    }

    /*
     * Decrease counter of one specific level
     */
    public void DecreaseLevelCounter(int level)
    {
        level_counter[level]--;
    }

    /*
     * Disable handles' collider for the actual level and
     * move landscape to next level
     */
    private void StartNextLevel(int general_counter)
    {
        foreach(GameObject rock in levels[general_counter - 1])
        {
            rock.GetComponent<Collider>().enabled = false;
        }

        movement = true;
    }


    /*
     * Move landscape to next level
     */
    private void MoveLandscape()
    {
        landscape.transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (landscape.transform.position.y < centers[general_counter].transform.position.y)
            movement = false;
    }

    /*
     * Ensure handles of given level are activated. 
     */
    private void SetAllActive(int level)
    {
        foreach (GameObject rock in levels[level])
        {
            rock.GetComponent<Handles>().ActivateColor();
        }
    }

    /*
     * Display "You win" text and load next scene
     */
    public void GameOver()
    {
        fadeOut = true;
        StartCoroutine(ChangeSceneRoutine("Play_Again", 3.0f));

    }

    private IEnumerator ChangeSceneRoutine(string scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(scene);

    }

    public void ShowWinningText()
    {
        fadeIn = true;
    }

    private void FallDown()
    {
        landscape.transform.Translate(Vector3.up * Time.deltaTime * fallingSpeed);

        if (landscape.transform.position.y > centers[0].transform.position.y)
        {
            restart = false;
            SceneManager.LoadScene("Mountain");
        }
            
    }
}
