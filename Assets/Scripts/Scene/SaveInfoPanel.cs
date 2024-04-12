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

    private void OnEnable()
    {
        InitPanelInfo();
    }

    /// <summary>
    /// 激活后初始化信息
    /// </summary>
    private void InitPanelInfo()
    {
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

        choicePanel.GetComponent<SaveChoosePanel>().save = save;
    }

    public void OnSelected()
    {
        // TODO: 动画显示
        choicePanel.SetActive(true);
    }
}
