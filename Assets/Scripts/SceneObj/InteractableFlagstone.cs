using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableFlagstone : MonoBehaviour,IInteract
{
    public Sprite sp1;
    public Sprite sp2;
    private SpriteRenderer _sr;
    public string[] strings1;
    public string[] strings2;
    public Transformable transformable;
    public bool _IsCleaned = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = sp1;
    }

    public void Interact(CharacterBehavior interactor)
    {
        if(_IsCleaned == false)
        {
            if (interactor.CurrentElement == Element.Water)
            {
                Debug.Log("清洗被尘埃覆盖的石板。");
                Debug.Log("显示游戏背景与元素特性。");
                _sr.sprite = sp2;
                _IsCleaned = true;
                transformable.SetState(_IsCleaned); // 更新 Transformable 的清洁状态
                interactor.ResetElement();
            }
            else
            {
                DialogueManager.instance.ShowDialogue(strings1);
                Debug.Log("石板被尘埃覆盖了，也许 *水* 元素进行清洗");
                transformable.SetState(_IsCleaned); // 更新 Transformable 的清洁状态
            }
        }
        else
        {
            Debug.Log("显示游戏背景与元素特性。");
            transformable.ShowPrompt();
            DialogueManager.instance.ShowDialogue(strings2);
            //interactionPromptImage.SetActive(false); // 隐藏提示框当对话开始时
        }
    }
}
