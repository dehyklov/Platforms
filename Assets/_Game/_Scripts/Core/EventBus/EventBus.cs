using System;

public class EventBus
{
    private static EventBus _instance;

    public static EventBus Instance
    {
        
        get 
        {
            if(_instance == null)
                _instance = new EventBus();
            return _instance; 
        }
    }

    public Action OnButtonClicked;
    public Action OnScreenChanged;
    public Action OnPlayButtonPressed;
    public Action OnGameEnd;
    public Action OnPlayerWon;
    public Action OnPlayerLose;
}
