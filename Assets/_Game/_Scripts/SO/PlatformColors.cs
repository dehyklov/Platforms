using UnityEngine;

[CreateAssetMenu(fileName = "PlatformColors", menuName = "Scriptable Objects/PlatformColors")]
public class PlatformColors : ScriptableObject
{
    [SerializeField]
    private Color[] _colors =
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.cyan
    };

    public Color[] Colors { get { return _colors; } }
}
