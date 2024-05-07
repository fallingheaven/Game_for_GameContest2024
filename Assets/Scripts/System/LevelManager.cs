using Utility.CustomClass;

public class LevelManager : Singleton<LevelManager>
{
    public GameSceneSO currentLevel;

    public void ToNextLevel()
    {
        currentLevel = currentLevel.nextScene;
    }
    
    public void ToLastLevel()
    {
        currentLevel = currentLevel.lastScene;
    }

    public void RefreshLevel()
    {
        StartCoroutine(SceneLoadManager.Instance.LoadSceneAsync(currentLevel, true));
    }
}
