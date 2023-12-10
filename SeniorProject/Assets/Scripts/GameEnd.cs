using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private GameObject gameOverScreen;
    private GameObject gameWinScreen;

    private static bool gameFinished;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
        gameWinScreen = GameObject.FindGameObjectWithTag("GameWin");
        gameOverScreen.SetActive(false);
        gameWinScreen.SetActive(false);
        gameFinished = false;
    }

    // make game over screen visible
    public void GameOver()
    {
        if (!gameFinished)
        {
            gameOverScreen.SetActive(true);
            gameFinished = true;
        }
    }

    // make game win screen visible
    public void GameWin()
    {
        if (!gameFinished)
        {
            gameWinScreen.SetActive(true);
            gameFinished = true;
        }
    }
    
    // return true if game finished, false otherwise
    public static bool IsGameFinished()
    {
        return gameFinished;
    }
}
