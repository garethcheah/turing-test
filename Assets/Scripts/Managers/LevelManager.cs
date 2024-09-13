using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isFinalLevel;

    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;

    public void StartLevel()
    {
        OnLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        OnLevelEnd?.Invoke();

        if (_isFinalLevel)
        {
            GameManager.instance.ChangeState(GameManager.GameState.GameEnd, this);
        }
        else
        {
            GameManager.instance.ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }
}
