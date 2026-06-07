using UnityEngine;

public class PlayButtonTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _canvasGroup.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvasGroup.alpha = 0;
        }
    }
}
