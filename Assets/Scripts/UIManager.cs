using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    // Hearts
    public GameObject H1;
    public GameObject H2;
    public GameObject H3;
    // Diamonds
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    // Counts
    private int hearts;
    private int diamonds;


    /*
    public Text diamondsCollectedText;
    public Text hitsTakenText;
    public GameObject gameOverWindow;
    */

    private void Start()
    {
        D1.GetComponent<ColorRenderer>().InactiveColor();
        D2.GetComponent<ColorRenderer>().InactiveColor();
        D3.GetComponent<ColorRenderer>().InactiveColor();

        H1.GetComponent<ColorRenderer>().ActiveColor();
        H2.GetComponent<ColorRenderer>().ActiveColor();
        H3.GetComponent<ColorRenderer>().ActiveColor();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }


    public void UpdateDiamondsCollected(bool addDiamond)
    {
        diamonds = GameManager.Instance.diamondsCollected;
        SetDiamonds(addDiamond);
    }

    public void UpdateHitsTaken()
    {
        hearts = GameManager.Instance.livesLost;
        RemoveHearts();
    }

    public void ShowGameOverWindow()
    {
        //gameOverWindow.SetActive(true);
    }

    private void SetDiamonds(bool addDiamond)
    {
        if (addDiamond)
        {
            switch (diamonds)
            {
                case 1:
                    D1.GetComponent<ColorRenderer>().ActiveColor();
                    break;
                case 2:
                    D2.GetComponent<ColorRenderer>().ActiveColor();
                    break;
                case 3:
                    D3.GetComponent<ColorRenderer>().ActiveColor();
                    // FER "YOU WIN" o algo aixi
                    break;
            }
        }

        else if (!addDiamond)
        {
            switch (diamonds)
            {
                case 0:
                    D1.GetComponent<ColorRenderer>().InactiveColor();
                    break;
                case 1:
                    D2.GetComponent<ColorRenderer>().InactiveColor();
                    break;
                case 2:
                    D3.GetComponent<ColorRenderer>().InactiveColor();
                    // FER "YOU WIN" o algo aixi
                    break;
            }
        }
    }

    private void RemoveHearts()
    {
        switch (hearts)
        {
            case 1:
                H1.GetComponent<ColorRenderer>().InactiveColor();
                break;
            case 2:
                H2.GetComponent<ColorRenderer>().InactiveColor();
                break;
            case 3:
                H3.GetComponent<ColorRenderer>().InactiveColor();
                // FER "YOU LOSE" o algo aixi
                break;

        }
    }

}
