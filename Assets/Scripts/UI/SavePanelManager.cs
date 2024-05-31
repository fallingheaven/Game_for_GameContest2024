using System.Collections;
using System.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
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
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                for (var i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            });
            
            await GetSaves();
            
            // Instantiate方法只能在主线程中使用
            UnityMainThreadDispatcher.Instance().Enqueue(ShowSaveInOrder());
        }
    
        private async Task GetSaves()
        {
            _saveArray = await Task.Run(() => SaveLoadManager.Instance.GetSaveArray);
        }
    
        private IEnumerator ShowSaveInOrder()
        {
            ResizeViewport();
            
            var choicePanel = Instantiate(choicePanelPrefab, transform.parent);
            
            //UI的显示初始化
            foreach (var save in _saveArray.saves)
            {
                var panelObj = Instantiate(saveInfoPanelPrefab, transform);
                var panel = panelObj.GetComponent<SaveInfoPanel>();
                
                panel.save = save;
                panel.choicePanel = choicePanel;

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void ResizeViewport()
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 200f * _saveArray.saves.Count);
        }

    #endregion

    public void StartNewGame()
    {
        GameManager.Instance.StartNewGame();
    }
}
