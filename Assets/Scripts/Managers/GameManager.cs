using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // Singleton
{
    public static GameManager instance;

    [SerializeField] private LevelManager[] _levels;

    private GameState _currentState;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }

    public bool IsInputActive()
    {
        return _isInputActive;
    }

    public void ChangeState(GameState state, LevelManager level)
    {
        _currentState = state;
        _currentLevel = level;

        switch (_currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitiateLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                EndGame();
                break;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Go to the first state
        if (_levels.Length > 0)
        {
            ChangeState(GameState.Briefing, _levels[_currentLevelIndex]);
        }
    }

    private void StartBriefing()
    {
        // Disable player input
        _isInputActive = false;

        // Start the level
        ChangeState(GameState.LevelStart, _levels[_currentLevelIndex]);
    }

    private void InitiateLevel()
    {
        _isInputActive = true;
        _currentLevel.StartLevel();

        ChangeState(GameState.LevelIn, _levels[_currentLevelIndex]);
    }

    private void RunLevel()
    {
        // Change state
    }

    private void CompleteLevel()
    {
        ChangeState(GameState.LevelStart, _levels[_currentLevelIndex++]);
    }

    private void GameOver()
    {

    }

    private void EndGame()
    {

    }
}
