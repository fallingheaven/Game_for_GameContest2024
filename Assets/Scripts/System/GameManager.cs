using System;
using SaveLoad;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SaveLoadManager _saveLoadManager;

    private void Awake()
    {
        _saveLoadManager = gameObject.AddComponent<SaveLoadManager>();
    }

    public void StartNewGame()
    {
        _saveLoadManager.AddSave(new GameSave());

        //TODO:跳转到游戏开局
    }

    public void LoadSave()
    {
        
    }
}
