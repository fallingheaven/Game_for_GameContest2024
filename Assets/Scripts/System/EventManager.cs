using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.CustomClass;
using Utility.Interface;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<int, List<Action<IEventMessage>>> _eventHandler = 
        new Dictionary<int, List<Action<IEventMessage>>>();
    
    private Dictionary<int, List<Action<IEventMessage>>> _eventHandlerAsync =
        new Dictionary<int, List<Action<IEventMessage>>>();
    
    
    #region 订阅事件

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isAsync"></param>
        /// <typeparam name="T"></typeparam>
        public void SubscribeEvent<T>(Action<IEventMessage> action, bool isAsync = false) where T : IEventMessage
        {
            var eventType = typeof(T);
            var eventID = eventType.GetHashCode();

            if (isAsync)
            {
                if (!_eventHandlerAsync.ContainsKey(eventID))
                {
                    _eventHandlerAsync[eventID] = new List<Action<IEventMessage>>();
                }

                _eventHandlerAsync[eventID].Add(action);
            }
            else
            {
                if (!_eventHandler.ContainsKey(eventID))
                {
                    _eventHandler[eventID] = new List<Action<IEventMessage>>();
                }

                _eventHandler[eventID].Add(action);
            }
        }

    #endregion

    #region 取消订阅

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void UnsubscribeEvent<T>(Action<IEventMessage> action) where T : IEventMessage
        {
            var eventType = typeof(T);
            var eventID = eventType.GetHashCode();

            if (_eventHandler.ContainsKey(eventID))
            {
                _eventHandler[eventID].Remove(action);
            }
            
            if (_eventHandlerAsync.ContainsKey(eventID))
            {
                _eventHandlerAsync[eventID].Remove(action);
            }
        }

    #endregion

    #region 调用事件

        /// <summary>
        /// 调用事件
        /// </summary>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        public async void InvokeEvent<T>(T message) where T : IEventMessage
        {
            var eventType = typeof(T);
            var eventID = eventType.GetHashCode();

            if (_eventHandler.TryGetValue(eventID, out var value))
            {
                foreach (var handler in value)
                {
                    handler.Invoke(message);
                }
            }
            
            if (_eventHandlerAsync.TryGetValue(eventID, out var valueAsync))
            {
                await Task.Run(() =>
                {
                    foreach (var handler in valueAsync)
                    {
                        handler.Invoke(message);
                    }
                });
            }
        }

    #endregion
}
