using UnityEngine;

public class EventOnlyInteractable : Interactable
{
    protected override void Interact()
    {
        Debug.Log("EventOnlyInteractable Interacted!");
    }
}
