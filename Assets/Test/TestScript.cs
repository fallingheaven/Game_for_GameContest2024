using SaveLoad;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameSceneSO targetScene;

    public void TeleportToTarget()
    {
        StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(targetScene));
    }

    public void AddNewSave()
    {
        var newSave = new GameSave();
        SaveLoadManager.Instance.AddSave(newSave);
    }
}
