using UnityEngine;
using SaveLoad;

public class ShowSaveUI : MonoBehaviour
{
    public GameObject saveInfoPrefab;

    public ActionCallbackEventSO<GameSaveArray> getSavesEventSO;

    private GameSaveArray _saveArray;

    public void ShowSaves()
    {
        getSavesEventSO.RaiseEvent(GetSaves);
    }

    private void GetSaves(GameSaveArray currentSaveArray)
    {
        _saveArray = currentSaveArray;
    }
}
