using UnityEngine;

public static class InputSystem
{
    /// <summary>
    /// 获取玩家移动输入
    /// </summary>
    public static Vector2 PlayerMoveInput
    {
        get
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            return new Vector2(horizontal, vertical);
        }
    }

    /// <summary>
    /// 获取玩家交互输入
    /// </summary>
    public static bool Interact => Input.GetKeyDown(KeyCode.E);

    /// <summary>
    /// 丢弃元素
    /// </summary>
    public static bool Release => Input.GetKeyDown(KeyCode.R);
}
