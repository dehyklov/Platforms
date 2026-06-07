using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactorSource;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _interactRange = 3f;

    [Header("Settings")]
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    private IInteractable _currentInteractable;
    private GameObject _currentHitObject;

    private void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(_interactKey))
        {
            PerformInteraction();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(_interactorSource.position, _interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactRange, _interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (_currentHitObject != hit.collider.gameObject)
                {
                    _currentInteractable = interactable;
                    _currentHitObject = hit.collider.gameObject;
                }
                return;
            }
        }

        if (_currentInteractable != null)
        {
            _currentInteractable = null;
            _currentHitObject = null;
        }
    }

    private void PerformInteraction()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }
}