using UnityEngine;

public class Introduction : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Introductin,press E to continue");
        Destroy(gameObject); // Removes object after interaction
    }
}

