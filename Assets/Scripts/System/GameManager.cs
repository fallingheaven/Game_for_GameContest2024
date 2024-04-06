using System;
using SaveLoad;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSceneSO newGameScene;
    
    private SaveLoadManager _saveLoadManager;
    private SceneLoadManager _sceneLoadManager;

    private void Awake()
    {
        _saveLoadManager = SaveLoadManager.Instance;
        _sceneLoadManager = SceneLoadManager.Instance;
    }

    public void StartNewGame()
    {
        var newSave = new GameSave();
        _saveLoadManager.AddSave(newSave);
        _saveLoadManager.LoadSave(newSave);

        //DONE:跳转到游戏开局
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(newGameScene));
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
