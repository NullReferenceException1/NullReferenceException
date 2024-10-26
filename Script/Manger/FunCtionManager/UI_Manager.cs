
using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
///UI管理器
/// </summary>
public class UI_Manager : GlobalManagerBase<UI_Manager>
{
    private string path = "UI/";
    private Dictionary<string, UI_WindowBase> windowDic = new Dictionary<string, UI_WindowBase>();//UI窗口缓存池
    public Transform UI_Layer_1;
    public Transform UI_Layer_2;

    /// <summary>
    /// 显示UI窗口
    /// </summary>
    /// <typeparam name="U">窗口类型</typeparam>
    /// <param name="transitionTime">淡入的过渡时间</param>
    /// <param name="layer">所处层级</param>
    /// <returns></returns>
    public U ShowWindow<U>(float transitionTime = 0, EnumUI_Layer layer = EnumUI_Layer.Layer_1) where U:UI_WindowBase
    {
        string name = typeof(U).Name;
        if (!windowDic.TryGetValue(name, out UI_WindowBase window))
        {
            GameObject obj = (GameObject)Resources.Load(path + name);
            Transform root = CheackLayer(layer);
            window = Instantiate(obj, root).GetComponent<UI_WindowBase>();
            windowDic.Add(name, window);
        }
        window.OnShow(transitionTime);
        return window as U;
    }
   

    /// <summary>
    /// 关闭UI窗口
    /// </summary>
    /// <typeparam name="U">窗口类型</typeparam>
    /// <param name="isFade">是否需要淡出</param>
    /// <param name="transitionTime">淡出的过渡时间</param>
    /// <returns></returns>
    public void CloseWindow<U>(float transitionTime = 0) where U : UI_WindowBase
    {
        string name = typeof(U).Name;
        if (windowDic.Remove(name, out UI_WindowBase window))
        {
            window.OnClose(transitionTime);
            if(transitionTime==0) Destroy(window.gameObject);//需要淡出时，由UI窗口自行决定关闭时机
        }
    }
    /// <summary>
    /// 关闭所有窗口
    /// </summary>
    public void CloseAllWindow()
    {
        foreach (UI_WindowBase window in windowDic.Values)//遍历所有窗口
        {
            window.OnClose();
            Destroy(window.gameObject);
        }
        windowDic.Clear();//清空
    }
    public void CloseAllWindow(EnumUI_Layer layer)
    {


        foreach (Transform item in CheackLayer(layer))//遍历所有窗口
        {
            string name = item.GetComponent<UI_WindowBase>().name;
            if (windowDic.Remove(name.Replace("(Clone)", ""), out UI_WindowBase window))
            {
                window.OnClose();
                Destroy(window.gameObject);
            }
        }
    }
   

    /// <summary>
    /// 检查UI层级
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    private Transform CheackLayer(EnumUI_Layer layer)
    {
        switch (layer)
        {
            case EnumUI_Layer.Layer_1:
                return UI_Layer_1;
            case EnumUI_Layer.Layer_2:
                return UI_Layer_2;
                default: 
            return null;
        }
    }
}
