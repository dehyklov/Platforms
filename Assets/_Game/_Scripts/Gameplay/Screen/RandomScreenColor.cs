using UnityEngine;

public class RandomScreenColor : MonoBehaviour
{
    [SerializeField] private PlatformColors _platformColors;
    [SerializeField] private Renderer _renderer;

    public Color CurrentColor { get; private set; }

    public void SetRandomScreenColor()
    {
        int randomIndex = Random.Range(0, _platformColors.Colors.Length);

        CurrentColor = _platformColors.Colors[randomIndex];

        _renderer.material.color = CurrentColor;

        EventBus.Instance.OnScreenChanged?.Invoke();
    }
}