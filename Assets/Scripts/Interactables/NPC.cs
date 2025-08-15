using UnityEngine;

public class NPC : Interactable
{
    protected override void Interact()
    {
        Debug.Log("You have found a lost item!");
        Destroy(gameObject); // Removes object after interaction
    }
}
