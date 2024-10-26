using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �¼���Ϣ����(���ڼ�¼ ��ͬ�����¼� �Ķಥί��)
/// </summary>
public class EventInfoBase { }
/// <summary>
/// �޲��� �¼���Ϣ
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
/// һ������ �¼���Ϣ
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
///�¼�������
/// </summary>
public class Event_Manager : GlobalManagerBase<Event_Manager>
{
    //�����¼�
    //����¼�
    //�Ƴ��¼�
    //�Ƴ�ȫ���¼�
    private Dictionary<EnumEventType, EventInfoBase> eventDic = new Dictionary<EnumEventType, EventInfoBase>();
    #region �޲������¼�����
    /// <summary>
    /// ���� �޲��� �¼�
    /// </summary>
    /// <param name="enumName"></param>
    public void EventTrigger(EnumEventType enumName)
    {
        if(eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo).action?.Invoke();
        }
        else Debug.LogError("��Ҫ�������¼�������");
    }
    /// <summary>
    /// ��� �޲��� �¼�����
    /// </summary>
    /// <param name="enumName">�¼�����</param>
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
    /// �Ƴ� �޲��� �¼�����
    /// </summary>
    /// <param name="enumName">�¼�����</param>
    public void RemoveEventListener(EnumEventType enumName, UnityAction action)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo).action -= action;
        }
        else Debug.LogError("��Ҫ�Ƴ����¼�������");
    }
    #endregion
    #region �в������¼�����
    /// <summary>
    /// ���� һ������ �¼�
    /// </summary>
    /// <param name="enumName"></param>
    public void EventTrigger<T>(EnumEventType enumName,T info)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo<T>).action?.Invoke(info);
        }
        else Debug.LogError("��Ҫ�������¼�������");
    }
    /// <summary>
    /// ��� һ������ �¼�����
    /// </summary>
    /// <param name="enumName">�¼�����</param>
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
    /// �Ƴ� һ������ �¼�����
    /// </summary>
    /// <param name="enumName">�¼�����</param>
    public void RemoveEventListener<T>(EnumEventType enumName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(enumName))
        {
            (eventDic[enumName] as EventInfo<T>).action -= action;
        }
        else Debug.LogError("��Ҫ�Ƴ����¼�������");
    }
    #endregion
}
