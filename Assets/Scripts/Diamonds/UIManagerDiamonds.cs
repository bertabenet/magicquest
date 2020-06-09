using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerDiamonds : MonoBehaviour
{
    public static UIManagerDiamonds Instance;
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

    public GameObject winText;
    public GameObject loseText;

    private void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        D1.GetComponent<ColorRenderer>().InactiveColor();
        D2.GetComponent<ColorRenderer>().InactiveColor();
        D3.GetComponent<ColorRenderer>().InactiveColor();

        H1.GetComponent<ColorRenderer>().ActiveColor();
        H2.GetComponent<ColorRenderer>().ActiveColor();
        H3.GetComponent<ColorRenderer>().ActiveColor();
    }

    void Awake()
    {
        Instance = this;
    }

    public void UpdateDiamondsCollected(bool addDiamond)
    {
        diamonds = GameManagerDiamonds.Instance.diamondsCollected;
        SetDiamonds(addDiamond);
    }

    public void UpdateHitsTaken()
    {
        hearts = GameManagerDiamonds.Instance.livesLost;
        RemoveHearts();
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
                break;

        }
    }

    public void YouWin()
    {
        winText.SetActive(true);
    }

    public void YouLose()
    {
        loseText.SetActive(true);
    }
}
