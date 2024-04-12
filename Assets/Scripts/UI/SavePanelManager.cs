using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using SaveLoad;
using UnityEngine.UI;
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

            Debug.Log(0);
            await ShowSaveInOrder();
        }
    
        private async Task GetSaves()
        {
            _saveArray = await Task.Run(() => SaveLoadManager.Instance.GetSaveArray);
        }
    
        private async Task ShowSaveInOrder()
        {
            Debug.Log(0.5f);
            var choicePanel = Instantiate(choicePanelPrefab, transform);
            Debug.Log(choicePanel == null);
            Debug.Log(1);
            
            //UI的显示初始化
            foreach (var save in _saveArray.saves)
            {
                var panelObj = Instantiate(saveInfoPanelPrefab, transform);
                var panel = panelObj.GetComponent<SaveInfoPanel>();
                
                panel.save = save;
                panel.choicePanel = choicePanel;

                await Task.Delay(10);// 等待10毫秒
            }
        }

    #endregion
    
}
