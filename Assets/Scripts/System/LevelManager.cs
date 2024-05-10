using Utility.CustomClass;

public class LevelManager : Singleton<LevelManager>
{
    public GameSceneSO currentLevel;

    public void ToNextLevel()
    {
        currentLevel = currentLevel.nextScene;
        LoadLevel();
    }
    
    public void ToLastLevel()
    {
        currentLevel = currentLevel.lastScene;
        LoadLevel();
    }

    public void RefreshLevel()
    {
        StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(currentLevel, true));
    }

    private void LoadLevel()
    {
        StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(currentLevel, false));
    }
}
