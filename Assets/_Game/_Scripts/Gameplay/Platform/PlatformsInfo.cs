using UnityEngine;

public class PlatformsInfo : MonoBehaviour
{
    [SerializeField] private PlatformColor[] _platforms;
    [SerializeField] private RandomScreenColor _randomScreenColor;

    public void CheckPlatformsColors()
    {
        Color screenColor = _randomScreenColor.CurrentColor;

        bool hasSameColor = false;

        foreach (PlatformColor platform in _platforms)
        {
            if (platform.GetColor() == screenColor)
            {
                hasSameColor = true;
                break;
            }
        }

        if (!hasSameColor)
        {
            int randomIndex = Random.Range(0, _platforms.Length);

            _platforms[randomIndex].SetColor(screenColor);
        }
    }

    public void SwitchOffPlatforms()
    {
        Color screenColor = _randomScreenColor.CurrentColor;

        foreach (PlatformColor platform in _platforms)
        {
            if (platform.GetColor() != screenColor)
            {
                platform.gameObject.SetActive(false);
            }
        }
    }
    public void SwitchOnPlatforms()
    {
        foreach (PlatformColor platform in _platforms)
        {
            platform.gameObject.SetActive(true);
        }
    }
}