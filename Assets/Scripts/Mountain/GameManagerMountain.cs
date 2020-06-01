using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMountain : MonoBehaviour
{
    public static GameManagerMountain Instance;
   
    public List<GameObject> level0 = new List<GameObject>();                    // rocks in level 0
    public List<GameObject> level1 = new List<GameObject>();                    // rocks in level 1
    public List<GameObject> level2 = new List<GameObject>();                    // rocks in level 2

    private int general_counter;                                                // increase once a level is complete
    private List<List<GameObject>> levels = new List<List<GameObject>>();       // keep track of all the levels' gameobjects
    private List<int> level_counter = new List<int>();                          // keep track of correct handles in each level

    public GameObject landscape;                                                // landscape object
    public List<GameObject> centers = new List<GameObject>();                   // center for each level on the landscape
    public int speed;                                                           // speed to move the landscape
    private bool movement;                                                      // move landscape


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        levels.Add(level0);
        levels.Add(level1);
        levels.Add(level2);
        for (int i = 0; i < levels.Count; i++)
        {
            level_counter.Add(0);
        }
        movement = false;
    }

    private void Update()
    {
        if (movement)
        {
            Debug.Log("Level = " + general_counter);
            Debug.Log("New center = " + centers[general_counter].transform.position.y);

            landscape.transform.Translate(Vector3.down * Time.deltaTime * speed);

            if(landscape.transform.position.y < centers[general_counter].transform.position.y)
                movement = false;
            
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

    private void StartNextLevel(int general_counter)
    {
        Debug.Log("NEXT LEVEL!!");
        foreach(GameObject rock in levels[general_counter - 1])
        {
            rock.GetComponent<Collider>().enabled = false;
        }
        
        MoveLandscape();
    }

    private void MoveLandscape()
    {
        Debug.Log("START MOVING");
        movement = true;
    }

    private void GameOver()
    {
        //UIManager.Instance.SetGameOverText();

        /*
        // stop collider
        positionWristL.GetComponent<Collider>().enabled = false;
        positionWristR.GetComponent<Collider>().enabled = false;
        positionAnkleL.GetComponent<Collider>().enabled = false;
        positionAnkleR.GetComponent<Collider>().enabled = false;

        // set color to gray forever
        positionWristL.GetComponent<MeshRenderer>().material.color = Color.gray;
        positionWristR.GetComponent<MeshRenderer>().material.color = Color.gray;
        positionAnkleL.GetComponent<MeshRenderer>().material.color = Color.gray;
        positionAnkleR.GetComponent<MeshRenderer>().material.color = Color.gray;
        */
    }
}
