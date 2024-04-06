using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SaveLoad;
using Utility.CustomClass;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private GameSaveArray _saveArray;
    private GameSave _currentSave;
    private const string SAVEPATH = "Assets/Resources/Saves/";
    private const string SAVEARRAY = "saveDataArray.json";

    [Header("事件监听")]
        private ActionCallbackEventSO getSavesEventSO;

    private void Awake()
    {
        InitSaveArray();
        InitEventSO();
    }

    private void OnEnable()
    {
        getSavesEventSO.onEventRaised += RequestForData;
    }

    private void OnDisable()
    {
        getSavesEventSO.onEventRaised -= RequestForData;
    }

    #region 初始化

        /// <summary>
        /// 预加载存档数组
        /// </summary>
        private void InitSaveArray()
        {
            _saveArray = new GameSaveArray();
            var filePath = SAVEPATH + SAVEARRAY;
            if (File.Exists(filePath))
            {
                Debug.Log("加载存档json文件");
                var json = File.ReadAllText(filePath);
                _saveArray = JsonUtility.FromJson<GameSaveArray>(json);
            }
            else
            {
                Debug.Log("新建存档json文件");
                var json = JsonUtility.ToJson(_saveArray);
                File.WriteAllText(filePath, json);
            }
 
            _saveArray.saves.onChanged += UpdateSaveArrayJson;
        }

        private void InitEventSO()
        {
            getSavesEventSO = Resources.Load<ActionCallbackEventSO>("Events/GetSavesEventSO");
        }

    #endregion

    #region 存档逻辑

        /// <summary>
        /// 添加存档
        /// </summary>
        /// <param name="newSave"></param>
        public void AddSave(GameSave newSave)
        {
            _saveArray.saves.Add(newSave);
        }
    
        /// <summary>
        /// 删除存档
        /// </summary>
        /// <param name="deletedSave"></param>
        public void RemoveSave(GameSave deletedSave) => _saveArray.saves.Remove(deletedSave);
    
        public void LoadSave(GameSave save) => _currentSave = save;
        
        /// <summary>
        /// 重置存档
        /// </summary>
        /// <param name="save"></param>
        public void ResetSave(GameSave save) => save.ResetSave();
    
        /// <summary>
        /// 当存档数量改变时更新json文件
        /// </summary>
        private void UpdateSaveArrayJson()
        {
            var json = JsonUtility.ToJson(_saveArray.saves);
            File.WriteAllText(SAVEPATH + SAVEARRAY, json);
        }

    #endregion

    /// <summary>
    /// 用于回调传递存档数组
    /// </summary>
    /// <param name="callback"></param>
    private void RequestForData(Action<object> callback)
    {
        if (callback is Action<GameSaveArray> gameSaveArrayCallback)
        {
            gameSaveArrayCallback.Invoke(_saveArray);
        }
        else
        {
            Debug.LogError("调取存档数据时回调函数传递有误");
        }
    }
}
