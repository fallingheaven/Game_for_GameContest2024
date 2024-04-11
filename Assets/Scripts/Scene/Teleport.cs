using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO targetScene;

    public void TeleportToTarget()
    {
        StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(targetScene));
    }
}
