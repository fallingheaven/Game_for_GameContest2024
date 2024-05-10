using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility;

[CreateAssetMenu(menuName = "GameScene")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference targetScene;
    public Vector3 initPosition;
    public GameSceneSO nextScene;
    public GameSceneSO lastScene;
    public SceneType type;
}
