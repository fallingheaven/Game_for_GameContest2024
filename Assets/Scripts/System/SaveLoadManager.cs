using System;
using System.IO;
using UnityEngine;
using SaveLoad;

public class SaveLoadManager : MonoBehaviour
{
    private GameSaveArray _saveArray;
    private GameSave _currentSave;
    private const string SAVEPATH = "Assets/Resources/Saves/";
    private const string SAVEARRAY = "saveDataArray.json";

    [Header("事件监听")]
    private static ActionCallbackEventSO<GameSaveArray> getSavesEventSO;

    private void Awake()
    {
        InitSaveArray();
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
                var json = File.ReadAllText(filePath);
                _saveArray = JsonUtility.FromJson<GameSaveArray>(json);
            }

            _saveArray.saves.onChanged += UpdateSaveArrayJson;
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
        public void RemoveSave(GameSave deletedSave)
        {
            _saveArray.saves.Remove(deletedSave);
        }
    
        public void LoadSave(GameSave save)
        {
            
        }
        
        /// <summary>
        /// 重置存档
        /// </summary>
        /// <param name="save"></param>
        public void ResetSave(GameSave save)
        {
            save.ResetSave();
        }
    
        /// <summary>
        /// 当存档数量改变时更新json文件
        /// </summary>
        private void UpdateSaveArrayJson()
        {
            var json = JsonUtility.ToJson(_saveArray.saves);
            File.WriteAllText(SAVEPATH + SAVEARRAY, json);
        }

    #endregion

    private void RequestForData(Action<GameSaveArray> callback)
    {
        callback(_saveArray);
    }
}
