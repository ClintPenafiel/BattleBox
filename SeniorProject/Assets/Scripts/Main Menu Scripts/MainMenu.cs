using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   // Load the game scene
   public void Play()
   {
      SceneManager.LoadScene("GameScene");
      FindObjectOfType<GoldManager>().ResetGold();
   }

   public void Quit()
   {
      Debug.Log("QUIT");
      Application.Quit(); //This does not show in Unity editor, hence the above line
   }

   // Load the main menu
   public void LoadMainMenu()
   {
      SceneManager.LoadScene("StartMenu");
   }
   
}
