using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string playerTag = "Player";

    private bool isOpen = false;
    private bool canUseDoor = true;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        EventBus.Instance.OnPlayButtonPressed += DisableDoor;
        EventBus.Instance.OnGameEnd += EnableDoor;
    }

    private void DisableDoor()
    {
        canUseDoor = false;

        if (isOpen)
        {
            animator.Play("DoorClose");
            isOpen = false;
        }
    }

    private void EnableDoor()
    {
        canUseDoor = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canUseDoor) return;

        if (other.CompareTag(playerTag) && !isOpen)
        {
            animator.Play("DoorOpen");
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canUseDoor) return;

        if (other.CompareTag(playerTag) && isOpen)
        {
            animator.Play("DoorClose");
            isOpen = false;
        }
    }

    private void OnDestroy()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnPlayButtonPressed -= DisableDoor;
            EventBus.Instance.OnGameEnd -= EnableDoor;
        }
    }
}