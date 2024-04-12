using System;
using System.Collections;
using System.IO;
using UnityEngine;
using SaveLoad;
using Utility.CustomClass;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private GameSave _currentSave;
    private const string SAVEPATH = "Assets/Resources/Saves/";
    private const string SAVEARRAY = "saveDataArray.json";

    public GameSaveArray GetSaveArray { get; private set; }

    private void Awake()
    {
        InitSaveArray();
    }

    #region 初始化

        /// <summary>
        /// 预加载存档数组
        /// </summary>
        private void InitSaveArray()
        {
            GetSaveArray = new GameSaveArray();
            var filePath = SAVEPATH + SAVEARRAY;
            if (File.Exists(filePath))
            {
                Debug.Log("加载存档json文件");
                var json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, GetSaveArray);
            }
            else
            {
                Debug.Log("新建存档json文件");
                var json = JsonUtility.ToJson(GetSaveArray);
                File.WriteAllText(filePath, json);
            }
 
            GetSaveArray.saves.onChanged += UpdateSaveArrayJson;
        }

    #endregion

    #region 存档逻辑

        /// <summary>
        /// 添加存档
        /// </summary>
        /// <param name="newSave"></param>
        public void AddSave(GameSave newSave)
        {
            GetSaveArray.saves.Add(newSave);
        }
    
        /// <summary>
        /// 删除存档
        /// </summary>
        /// <param name="deletedSave"></param>
        public void RemoveSave(GameSave deletedSave) => GetSaveArray.saves.Remove(deletedSave);
    
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
            var json = JsonUtility.ToJson(GetSaveArray);
            File.WriteAllText(SAVEPATH + SAVEARRAY, json);
        }

    #endregion
    
}
