using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.Interface;
using Utility.CustomClass;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _bgmSource;
    private Queue<AudioSource> _fxSources = new Queue<AudioSource>();

    // 切换BGM时的异步任务
    private UniTask _operation;
    private CancellationTokenSource _cts = new CancellationTokenSource();

    private void OnEnable()
    {
        // 订阅事件
        EventManager.Instance.SubscribeEvent<AudioPlayEvent>(PlayAudio);
    }

    private void OnDisable()
    {
        // 取消订阅事件
        EventManager.Instance.UnsubscribeEvent<AudioPlayEvent>(PlayAudio);
    }

    private void Start()
    {
        // 使用协程来延迟初始化音频源
        StartCoroutine(DelayedInitAudioSources());
    }

    private IEnumerator DelayedInitAudioSources()
    {
        // 延迟一帧
        yield return new WaitForSeconds(0.5f);

        InitAudioSources();
    }

    /// <summary>
    /// 初始化音频源
    /// </summary>
    private void InitAudioSources()
    {
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        
        for (var i = 1; i <= 15; i++)
        {
            _fxSources.Enqueue(gameObject.AddComponent<AudioSource>());
        }
        
        EventManager.Instance.InvokeEvent(GameManager.Instance.bgm);
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void PlayAudio(IEventMessage message)
    {
        if (message is not AudioPlayEvent msg) return;

        // Debug.Log(msg.clip.name);

        switch (msg.type)
        {
            case audioType.FX:
                var fxSource = _fxSources.Dequeue();
                if (fxSource.isPlaying)
                {
                    _fxSources.Enqueue(fxSource);

                    fxSource = gameObject.AddComponent<AudioSource>();
                }
                _fxSources.Enqueue(fxSource);
                
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
    /// 切换BGM的淡入淡出
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

        while (_bgmSource.volume < startVolume)
        {
            _bgmSource.volume += startVolume * Time.deltaTime / fadeDuration;
            await UniTask.Yield(ctk);
        }
        _bgmSource.volume = startVolume;
    }
}

[Serializable]
public class AudioPlayEvent : IEventMessage
{
    public AudioClip clip;
    public audioType type;
}

[System.Serializable]
public enum audioType
{
    BGM, FX
}
