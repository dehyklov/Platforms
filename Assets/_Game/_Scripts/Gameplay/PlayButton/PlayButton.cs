using UnityEngine;

public class PlayButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        EventBus.Instance.OnPlayButtonPressed?.Invoke();
    }
}
