using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.CustomClass;
using Utility.Interface;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _bgmSource;
    private Queue<AudioSource> _fxSources;

    // 切换bgm途中
    private UniTask _operation;
    private CancellationTokenSource _cts;
    
    private void OnEnable()
    {
        InitAudioSources();
        EventManager.Instance.SubscribeEvent<AudioPlayEvent>(PlayAudio);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeEvent<AudioPlayEvent>(PlayAudio);
    }

    /// <summary>
    /// 初始化播放器
    /// </summary>
    private void InitAudioSources()
    {
        _bgmSource = Instance.gameObject.AddComponent<AudioSource>();
        
        for (var i = 1; i <= 10; i++)
        {
            _fxSources.Enqueue(Instance.gameObject.AddComponent<AudioSource>());
        }
    }
    
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void PlayAudio(IEventMessage message)
    {
        if (message is not AudioPlayEvent msg) return;
        
        switch (msg.type)
        {
            case audioType.FX:
                var fxSource = _fxSources.Dequeue();
                if (fxSource.isPlaying)
                {
                    _fxSources.Enqueue(fxSource);
                    
                    fxSource = Instance.gameObject.AddComponent<AudioSource>();
                    
                    _fxSources.Enqueue(fxSource);
                }
                
                fxSource.clip = msg.clip;
                fxSource.Play();
                break;
            
            case audioType.BGM:
                
                if (_operation.Status == UniTaskStatus.Pending)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                    _cts = new CancellationTokenSource();
                }

                _operation = SwitchBGM(msg.clip, 0.5f, _cts.Token);
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException($"不存在的音频类型");
        }
    }

    /// <summary>
    /// 切换bgm的淡入淡出
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="fadeDuration"></param>
    /// <param name="ctk"></param>
    private async UniTask SwitchBGM(AudioClip clip, float fadeDuration, CancellationToken ctk)
    {
        var startVolume = _bgmSource.volume;
        while (_bgmSource.volume > 0)
        {
            _bgmSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            await UniTask.Yield(ctk);
        }
        
        _bgmSource.Stop();
        _bgmSource.clip = clip;
        _bgmSource.Play();
        
        while (_bgmSource.volume <startVolume)
        {
            _bgmSource.volume += startVolume * Time.deltaTime / fadeDuration;
            await UniTask.Yield(ctk);
        }
        _bgmSource.volume = startVolume;
    }
}

public class AudioPlayEvent : ScriptableObject, IEventMessage
{
    public AudioClip clip;
    public audioType type;
}

[System.Serializable]
public enum audioType
{
    BGM, FX
}