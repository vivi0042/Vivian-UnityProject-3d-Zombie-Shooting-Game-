using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeReturn = 3f; // Time before returning to the main menu
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Name of the main menu scene

    void Start()
    {
        // Ensure cursor is unlocked before returning to the menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Start the countdown to return to the main menu
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(delayBeforeReturn); // Wait for the specified time
        SceneManager.LoadScene(mainMenuSceneName); // Load the main menu scene
    }
}
