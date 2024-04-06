using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "GameScene")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference targetScene;
    public Vector3 initPosition;
}
