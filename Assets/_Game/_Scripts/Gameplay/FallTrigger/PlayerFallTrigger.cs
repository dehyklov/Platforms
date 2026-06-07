using UnityEngine;

public class PlayerFallTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            EventBus.Instance.OnPlayerLose?.Invoke();
        }
    }
}