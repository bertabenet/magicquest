using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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

        winText.SetActive(false);
    }

    private void Update()
    {
        // move to next level when movement is true
        if (movement) MoveLandscape();
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
        winText.SetActive(true);
        StartCoroutine(ChangeSceneRoutine("Play_Again", 4.0f));

    }

    private IEnumerator ChangeSceneRoutine(string scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(scene);

    }
}
