using System;
using System.Collections;
using UnityEngine;
using SaveLoad;

public class ShowSaveUI : MonoBehaviour
{
    public GameObject saveInfoPrefab;

    [Header("事件调用")]
        public ActionCallbackEventSO getSavesEventSO;

    private GameSaveArray _saveArray;

    private bool _isFinished; // 获取存档是否完成，防止访问到null

    private void OnEnable()
    {
        InitEventSO();
    }

    private void InitEventSO()
    {
        getSavesEventSO = Resources.Load<ActionCallbackEventSO>("Events/GetSavesEventSO");
    }

    #region 显示Save的UI

        public void ShowSaves()
        {
            _isFinished = false;
            getSavesEventSO.RaiseEvent(GetSaves);
            StartCoroutine(ShowSaveInOrder());
        }
    
        private void GetSaves(object currentSaveArray)
        {
            _saveArray = currentSaveArray as GameSaveArray;
            _isFinished = true;
        }
    
        private IEnumerator ShowSaveInOrder()
        {
            yield return new WaitUntil(() => _isFinished);
            
            //TODO:UI的显示初始化
        }

    #endregion
    
}
