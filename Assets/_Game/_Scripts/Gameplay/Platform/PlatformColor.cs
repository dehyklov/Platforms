using UnityEngine;

public class PlatformColor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private PlatformColors _platformColors;

    private void Start()
    {
        EventBus.Instance.OnScreenChanged += SetRandomColor;
    }

    private void SetRandomColor()
    {
        int randomIndex = Random.Range(0, _platformColors.Colors.Length);

        _renderer.material.color = _platformColors.Colors[randomIndex];
    }

    public Color GetColor()
    {
        return _renderer.material.color;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void OnDestroy()
    {
        EventBus.Instance.OnScreenChanged -= SetRandomColor;
    }
}