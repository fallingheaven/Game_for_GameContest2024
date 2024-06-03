using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveInfoPanel : MonoBehaviour
{
    public GameSave save = null;
    public GameObject choicePanel;
    
    public TextMeshProUGUI levelInfo;
    public TextMeshProUGUI playTimeInfo;
    public TextMeshProUGUI startNewGame;

    // private void OnEnable()
    // {
    //     InitPanelInfo();
    // }

    /// <summary>
    /// 激活后初始化信息
    /// </summary>
    public void InitPanelInfo()
    {
        // Debug.Log(save.levelIndex);
        if (save != null)
        {
            startNewGame.text = "";
            
            levelInfo.text = $"Level: {save.levelIndex}";
            playTimeInfo.text = $"play time: {save.playTime}";
        }
        else
        {
            startNewGame.text = "Start New Game";
            levelInfo.text = "";
            playTimeInfo.text = "";
        }
        
    }

    public void OnSelected()
    {
        // TODO: 动画显示
        choicePanel.GetComponent<SaveChoosePanel>().save = save;
        choicePanel.SetActive(true);
    }
}
