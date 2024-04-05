using SaveLoad;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    [Tooltip("对选中存档做处理选择的面板")]
    public GameObject choosePanel;
    
    private GameSave _thisSave;

    public void OnSelected()
    {
        choosePanel.SetActive(true);
        
        // TODO: 显示选择面板动画（建议用Dotween插件做）
    }

    public void OnLoad()
    {
        // TODO: 对游戏管理器发出加载场景消息
    }
    
    public void OnDeleted()
    {
        // TODO: 对游戏管理器发出删除存档消息
    }

    public void OnReset()
    {
        // TODO: 对游戏管理器发出重置存档消息
    }
}
