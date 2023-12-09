using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private GameObject gameOverScreen;
    private GameObject gameWinScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
        gameWinScreen = GameObject.FindGameObjectWithTag("GameWin");
        gameOverScreen.SetActive(false);
        gameWinScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void GameWin()
    {
        gameWinScreen.SetActive(true);
    }
}
