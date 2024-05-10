using PimDeWitte.UnityMainThreadDispatcher;
using SaveLoad;
using Utility.CustomClass;

public class GameManager : Singleton<GameManager>
{
    public GameSceneSO currentGameScene;
    public GameSceneSO newGameScene;
    public GameSceneSO[] gameSceneArray;
    
    // 为了创建实例
    private SaveLoadManager _saveLoadManager;
    private SceneLoadManager _sceneLoadManager;
    private EventManager _eventManager;

    private void Awake()
    {
        GameManager.Instance = this;
        _saveLoadManager = SaveLoadManager.Instance;
        _sceneLoadManager = SceneLoadManager.Instance;
        _eventManager = EventManager.Instance;
        gameObject.AddComponent<UnityMainThreadDispatcher>();
    }

    private void OnEnable()
    {
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(currentGameScene));
    }

    public void StartNewGame()
    {
        var newSave = new GameSave();
        _saveLoadManager.AddSave(newSave);
        _saveLoadManager.LoadSave(newSave);

        //DONE:跳转到游戏开局
        _sceneLoadManager.UnloadScene(currentGameScene);
        StartCoroutine(_sceneLoadManager.LoadSceneAsync(newGameScene));
    }

    #region 存档相关

        public void LoadSave(GameSave save)
        {
            _saveLoadManager.LoadSave(save);
            
            // DONE:跳转到游戏存档位置，注意顺序
            StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(gameSceneArray[save.levelIndex]));
            currentGameScene = gameSceneArray[save.levelIndex];
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
