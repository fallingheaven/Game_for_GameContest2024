using System;
using PimDeWitte.UnityMainThreadDispatcher;
using SaveLoad;
using UnityEngine;
using Utility;
using Utility.CustomClass;

public class GameManager : Singleton<GameManager>
{
    public GameObject gameSceneObjects;

    public GameSceneSO menuScene;
    public GameSceneSO currentGameScene;
    public GameSceneSO newGameScene;
    public GameSceneSO[] gameSceneArray;

    public GameObject playerCharacter;
    public GameObject virtualCamera;

    private float _currentPlayTime;
    
    // 为了创建实例
    private SaveLoadManager _saveLoadManager;
    private SceneLoadManager _sceneLoadManager;
    private EventManager _eventManager;
    private AudioManager _audioManager;
    
    public AudioPlayEvent bgm;

    private void Awake()
    {
        GameManager.Instance = this;
        _saveLoadManager = SaveLoadManager.Instance;
        _sceneLoadManager = SceneLoadManager.Instance;
        _eventManager = EventManager.Instance;
        _audioManager = AudioManager.Instance;
        gameObject.AddComponent<UnityMainThreadDispatcher>();
    }

    private void OnEnable()
    {
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(menuScene));
    }

    private void Update()
    {
        if (currentGameScene.type == SceneType.Level)
        {
            _currentPlayTime += Time.deltaTime;
        }
    }

    public void StartNewGame()
    {
        var newSave = new GameSave()
        {
            levelIndex = 1,
            playTime = 0f
        };
        _saveLoadManager.AddSave(newSave);
        _saveLoadManager.LoadSave(newSave);
        _currentPlayTime = 0f;

        //DONE:跳转到游戏开局
        _sceneLoadManager.UnloadScene(currentGameScene);
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(newGameScene));
    }

    public void RestartCurrentLevel()
    {
        LevelManager.Instance.RefreshLevel();
    }

    public void BackToMenu()
    {
        for (var i = 0; i < gameSceneArray.Length; i++)
        {
            if (currentGameScene == gameSceneArray[i])
            {
                Debug.Log(i + 1);
                SaveLoadManager.Instance.UpdateCurrentSave(i + 1, _currentPlayTime);
                break;
            }
        }
        
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(menuScene));
        // Debug.Log(2);
    }

    #region 存档相关

        public void LoadSave(GameSave save)
        {
            _saveLoadManager.LoadSave(save);
            _currentPlayTime = save.playTime;
            
            // DONE:跳转到游戏存档位置，注意顺序
            StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(gameSceneArray[save.levelIndex - 1]));
            // currentGameScene = gameSceneArray[save.levelIndex];
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

    #endregion
    
}
