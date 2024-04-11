using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using SaveLoad;

public class ShowSaveUI : MonoBehaviour
{
    public GameObject saveInfoPrefab;

    private GameSaveArray _saveArray;

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
            //TODO:UI的显示初始化

            yield return null;
        }

    #endregion
    
}
