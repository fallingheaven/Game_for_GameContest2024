using System;
using SaveLoad;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SaveLoadManager _saveLoadManager;

    private void Awake()
    {
        _saveLoadManager = GetComponent<SaveLoadManager>();
    }

    public void StartNewGame()
    {
        var newSave = new GameSave();
        _saveLoadManager.AddSave(newSave);
        _saveLoadManager.LoadSave(newSave);

        //TODO:跳转到游戏开局
    }

    public void LoadSave(GameSave save)
    {
        _saveLoadManager.LoadSave(save);
        
        // TODO:跳转到游戏存档位置
    }

    public void DeleteSave(GameSave save)
    {
        _saveLoadManager.RemoveSave(save);
        
        // TODO: 确认删除、删除成功提示等
    }

    public void ResetSave(GameSave save)
    {
        _saveLoadManager.ResetSave(save);
        
        // TODO: 确认重置，重置成功提示等
    }
}
