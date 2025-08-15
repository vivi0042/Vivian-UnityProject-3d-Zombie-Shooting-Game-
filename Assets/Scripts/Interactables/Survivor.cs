using UnityEngine;
using UnityEngine.SceneManagement;

public class Survivor : Interactable
{
    [SerializeField] private string winSceneName = "EndingScene"; // Updated scene name
    private bool isPlayerNearby = false; // Track if player is near

    void Update()
    {
        // Check if player is near and presses "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You have rescued the survivor! Loading Ending Scene...");
            WinGame();
        }
    }

    protected override void Interact()
    {
        Debug.Log("Press 'E' to rescue the survivor!");
    }

    private void WinGame()
    {
        // Load the "EndingScene"
        SceneManager.LoadScene(winSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Press 'E' to rescue the survivor!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}