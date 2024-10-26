using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 事件信息基类(用于记录 不同参数事件 的多播委托)
/// </summary>
public class EventInfoBase { }
/// <summary>
/// 无参数 事件信息
/// </summary>
public class EventInfo: EventInfoBase
{  
    public UnityAction action;
    public EventInfo(UnityAction action)
    {
        this.action = action;
    }
}
/// <summary>
/// 一个参数 事件信息
/// </summary>
public class EventInfo<T> : EventInfoBase
{
    public UnityAction<T> action;
    public EventInfo(UnityAction<T> action)
    {
        this.action = action;
    }
}
/// <summary>
///事件管理器
/// </summary>
public class Event_Manager : GlobalManagerBase<Event_Manager>
{
    //触发事件
    //添加事件
    //移除事件
    //移除全部事件
    private Dictionary<EnumEventType, EventInfoBase> eventDic = new Dictionary<EnumEventType, EventInfoBase>();
    #region 无参数的事件监听
    /// <summary>
    /// 触发 无参数 事件
    /// </summary>
    /// <param name="enumName"></param>
    public void EventTrigger(EnumEventType enumName)
    {
        if(eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo).action?.Invoke();
        }
        else Debug.LogError("想要触发的事件不存在");
    }
    /// <summary>
    /// 添加 无参数 事件监听
    /// </summary>
    /// <param name="enumName">事件名称</param>
    public void AddEventListener(EnumEventType enumName, UnityAction action)
    {
        if(eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo).action += action;
        }
        else
        {
            eventDic.Add(enumName, new EventInfo(action));
        }
    }
    /// <summary>
    /// 移除 无参数 事件监听
    /// </summary>
    /// <param name="enumName">事件名称</param>
    public void RemoveEventListener(EnumEventType enumName, UnityAction action)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo).action -= action;
        }
        else Debug.LogError("想要移除的事件不存在");
    }
    #endregion
    #region 有参数的事件监听
    /// <summary>
    /// 触发 一个参数 事件
    /// </summary>
    /// <param name="enumName"></param>
    public void EventTrigger<T>(EnumEventType enumName,T info)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo<T>).action?.Invoke(info);
        }
        else Debug.LogError("想要触发的事件不存在");
    }
    /// <summary>
    /// 添加 一个参数 事件监听
    /// </summary>
    /// <param name="enumName">事件名称</param>
    public void AddEventListener<T>(EnumEventType enumName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo<T>).action += action;
        }
        else
        {
            eventDic.Add(enumName, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 移除 一个参数 事件监听
    /// </summary>
    /// <param name="enumName">事件名称</param>
    public void RemoveEventListener<T>(EnumEventType enumName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo<T>).action -= action;
        }
        else Debug.LogError("想要移除的事件不存在");
    }
    #endregion
}
