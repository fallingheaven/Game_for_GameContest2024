using System.Collections.Generic;
using UnityEngine;
using Utility.CustomClass;
using Utility.Interface;

public class InputManager : Singleton<InputManager>
{
    private Queue<ICommand> _commandQueue;
    private float _commandTimeOut = 0.2f;

    // TODO: 当玩家进出可交互物体触发器时更新
    public IInteract interactObj;

    private void Update()
    {
        CommandExecute();
    }

    private void CommandExecute()
    {
        while (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            if (Time.time - command.CommandTime <= _commandTimeOut)
            {
                command.Execute();
            }
        }
    }

    public void AddCommand(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }
}