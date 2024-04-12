using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using SaveLoad;
using Utility.CustomClass;

public class SavePanelManager : Singleton<SavePanelManager>
{
    public GameObject saveInfoPanelPrefab;
    public GameObject choicePanelPrefab;

    private GameSaveArray _saveArray;

    private void Awake()
    {
        SavePanelManager.Instance = this;
    }

    private void OnEnable()
    {
        Task.Run(ShowSaves);
    }

    private void OnDisable()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    #region 显示Save的UI

        public async Task ShowSaves()
        {
            await GetSaves();
            
            StartCoroutine(ShowSaveInOrder());
        }
    
        private async Task GetSaves()
        {
            _saveArray = await Task.Run(() => SaveLoadManager.Instance.GetSaveArray);
        }
    
        private IEnumerator ShowSaveInOrder()
        {
            //UI的显示初始化
            foreach (var save in _saveArray.saves)
            {
                var panel = GameObject.Instantiate(saveInfoPanelPrefab, transform, false).GetComponent<SaveInfoPanel>();
                
                panel.save = save;
                panel.choicePanel = GameObject.Instantiate(choicePanelPrefab, transform, false);
            }
            
            yield return null;
        }

    #endregion
    
}
