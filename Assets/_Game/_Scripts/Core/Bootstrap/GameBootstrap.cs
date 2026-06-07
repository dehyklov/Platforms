using System.Collections;
using TMPro;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private RandomScreenColor _randomScreenColor;
    [SerializeField] private PlatformsInfo _platformsInfo;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerSpawnPoint;

    [Header("Settings")]
    [SerializeField] private int _prepareTime = 5;
    [SerializeField] private int _actionTime = 3;
    [SerializeField] private int _roundsCount = 3;

    [Header("Round Messages")]
    [SerializeField] private string _roundStartMessage = "Round {0}/{1}";
    [SerializeField] private string _roundCompleteMessage = "Round complete!";
    [SerializeField] private float _betweenRoundsDelay = 1.5f;

    [Header("Result")]
    [SerializeField] private GameObject _resultGOUI;
    [SerializeField] private TextMeshProUGUI _resultText;

    private Coroutine _gameRoutine;
    private bool _isGameRunning = false;
    private int _currentRound = 0;

    private void Awake()
    {
        EventBus.Instance.OnPlayerWon += HandleWin;
        EventBus.Instance.OnPlayerLose += HandleLose;
    }
    private void Start()
    {
        EventBus.Instance.OnPlayButtonPressed += StartGame;
        ResetUI();
    }
    private void HandleWin()
    {
        _player.SetActive(false);
        ShowResult("YOU WIN!");
    }

    private void HandleLose()
    {
        _player.SetActive(false);
        ShowResult("YOU LOSE!");
    }
    private void ShowResult(string text)
    {
        _resultGOUI.SetActive(true);
        _resultText.text = text;

        SetCursor(true);

        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
            _gameRoutine = null;
        }

        _isGameRunning = false;
    }
    private void ResetUI()
    {
        if (_timer != null)
            _timer.text = "";
    }

    private void StartGame()
    {
        if (_isGameRunning) return;

        SetCursor(false);

        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
            _gameRoutine = null;
        }

        _currentRound = 0;
        _isGameRunning = true;
        _gameRoutine = StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine()
    {
        for (int round = 1; round <= _roundsCount; round++)
        {
            _currentRound = round;

            _timer.text = string.Format(_roundStartMessage, round, _roundsCount);
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(SingleRoundRoutine());

            if (round < _roundsCount)
            {
                _timer.text = _roundCompleteMessage;
                yield return new WaitForSeconds(_betweenRoundsDelay);
            }
        }

        _timer.text = "Game Complete!\nWell done!";
        yield return new WaitForSeconds(2f);

        _timer.text = "";
        _isGameRunning = false;
        _gameRoutine = null;

        EventBus.Instance.OnPlayerWon?.Invoke();
        EventBus.Instance.OnGameEnd?.Invoke();
    }

    private IEnumerator SingleRoundRoutine()
    {
        for (int i = _prepareTime; i >= 1; i--)
        {
            _timer.text = $"Get ready...\n{i}";
            yield return new WaitForSeconds(1f);
        }

        _randomScreenColor.SetRandomScreenColor();
        _platformsInfo.CheckPlatformsColors();

        _timer.text = "Color on screen!\nMemorize it!";
        yield return new WaitForSeconds(1f);

        for (int i = _actionTime; i >= 1; i--)
        {
            _timer.text = $"Time to match!\n{i}";
            yield return new WaitForSeconds(1f);
        }

        _timer.text = "Platforms off!\nMake your choice!";
        _platformsInfo.SwitchOffPlatforms();

        yield return new WaitForSeconds(2f);

        _platformsInfo.SwitchOnPlatforms();
    }

    public void ForceStopGame()
    {
        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
            _gameRoutine = null;
        }
        _isGameRunning = false;
        _currentRound = 0;
        ResetUI();

        _platformsInfo.SwitchOnPlatforms();
    }
    public void RestartGame()
    {

        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
            _gameRoutine = null;
        }

        _isGameRunning = false;
        _currentRound = 0;

        ResetUI();

        SetCursor(false);

        if (_resultGOUI != null)
            _resultGOUI.SetActive(false);

        if (_resultText != null)
            _resultText.text = "";

        _platformsInfo.SwitchOnPlatforms();

        _randomScreenColor.SetRandomScreenColor();

        if (_player != null)
        {
            if (_playerSpawnPoint != null)
            {
                _player.transform.position = _playerSpawnPoint.position;
                _player.transform.rotation = _playerSpawnPoint.rotation;
            }

            _player.SetActive(true);
        }

        EventBus.Instance.OnGameEnd?.Invoke();
    }
    private void SetCursor(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        EventBus.Instance.OnPlayButtonPressed -= StartGame;
        EventBus.Instance.OnPlayerWon -= HandleWin;
        EventBus.Instance.OnPlayerLose -= HandleLose;

        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
        }
    }
}