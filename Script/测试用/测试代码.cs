using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///
/// </summary>

public class 测试代码 : MonoBehaviour
{
    public GameManager gameManager;
    public Event_Manager event_Manager;
    public UI_Manager ui_Manager;
    [Button("测试方法.显示UI")]
    public void ShowUI(UI_WindowBase windowBase,float time=0)
    {
      
            UI_Manager.Instance.ShowWindow<UI_GameMenuWindow>(time);
   
        
    }
    [Button("测试方法.关闭UI")]
    public void CloseUI(UI_WindowBase windowBase, float time = 0)
    {
        UI_Manager.Instance.CloseWindow<UI_GameMenuWindow>(time);
    }
    [FoldoutGroup("事件测试,无参数")]
    [Button("测试方法.订阅事件")]
    public void 订阅无参数事件()
    {
        event_Manager.AddEventListener(EnumEventType.无参数事件测试, 无参数测试事件);


    }
    [FoldoutGroup("事件测试,无参数")]
    [Button("测试方法.移除事件")]
    public void 移除无参数事件()
    {
        event_Manager.RemoveEventListener(EnumEventType.无参数事件测试, 无参数测试事件);
       
    }
    [FoldoutGroup("事件测试,无参数")]
    [Button("测试方法.触发事件")]
    public void 触发无参数()
    {
        event_Manager.EventTrigger(EnumEventType.无参数事件测试);
    }
    public void 无参数测试事件()
    {
        Debug.Log("测试成功");
    }

    [FoldoutGroup("事件测试,有参数")]
    [Button("测试方法.订阅事件")]
    public void 订阅有参数事件()
    {
        event_Manager.AddEventListener<int>(EnumEventType.有参数事件测试, 有参数测试事件);


    }
    [FoldoutGroup("事件测试,有参数")]
    [Button("测试方法.移除事件")]
    public void 移除有参数事件()
    {
        event_Manager.RemoveEventListener<int>(EnumEventType.有参数事件测试, 有参数测试事件);

    }
    [FoldoutGroup("事件测试,有参数")]
    [Button("测试方法.触发事件")]
    public void 触发有参数(int index)
    {
        
        event_Manager.EventTrigger<int>(EnumEventType.有参数事件测试, index);
    }
    public void 有参数测试事件(int index)
    {
        Debug.Log(index);
    }
}
