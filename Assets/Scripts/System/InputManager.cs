using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.CustomClass;
using Utility.Interface;

public class InputManager : Singleton<InputManager>
{
    private readonly Queue<ICommand> _commandQueue = new();
    private readonly HashSet<ICommand> _commandHashSet = new();
    private float _commandTimeOut = 500f;

    private void Update()
    {
        CommandExecute().Forget();
    }

    private async UniTaskVoid CommandExecute()
    {
        while (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            _commandHashSet.Remove(command);
            if (Time.time - command.CommandTime > _commandTimeOut) continue;
            
            command.Execute();
        }

        await UniTask.Yield();
    }

    public void AddCommand(ICommand command)
    {
        if (_commandHashSet.Contains(command)) return;
            
        _commandQueue.Enqueue(command);
        _commandHashSet.Add(command);
    }
}