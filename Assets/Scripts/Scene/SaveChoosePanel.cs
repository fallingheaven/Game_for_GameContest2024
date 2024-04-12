using System.Threading.Tasks;
using SaveLoad;
using UnityEngine;

public class SaveChoosePanel : MonoBehaviour
{
    public GameSave save;

    public void LoadSave()
    {
        GameManager.Instance.LoadSave(save);
    }
    
    public void DeleteSave()
    {
        GameManager.Instance.DeleteSave(save);
        
        RefreshPanel();
    }
    
    public void ResetSave()
    {
        GameManager.Instance.ResetSave(save);

        RefreshPanel();
    }
    
    private void RefreshPanel()
    {
        gameObject.SetActive(false);
        Task.Run(SavePanelManager.Instance.ShowSaves);
    }
}
