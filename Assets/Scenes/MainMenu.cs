using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    string newGameScene = "ProjTesting1";
    void Start()
    {
        // Ensure the cursor is visible and unlocked when returning to the Main Menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitApplication()
{
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode
    #else
        Application.Quit(); // Quit game
    #endif
}

}
  
