
using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
///UI������
/// </summary>
public class UI_Manager : GlobalManagerBase<UI_Manager>
{
    private string path = "UI/";
    private Dictionary<string, UI_WindowBase> windowDic = new Dictionary<string, UI_WindowBase>();//UI���ڻ����
    public Transform UI_Layer_1;
    public Transform UI_Layer_2;

    /// <summary>
    /// ��ʾUI����
    /// </summary>
    /// <typeparam name="U">��������</typeparam>
    /// <param name="transitionTime">����Ĺ���ʱ��</param>
    /// <param name="layer">�����㼶</param>
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
    /// �ر�UI����
    /// </summary>
    /// <typeparam name="U">��������</typeparam>
    /// <param name="isFade">�Ƿ���Ҫ����</param>
    /// <param name="transitionTime">�����Ĺ���ʱ��</param>
    /// <returns></returns>
    public void CloseWindow<U>(float transitionTime = 0) where U : UI_WindowBase
    {
        string name = typeof(U).Name;
        if (windowDic.Remove(name, out UI_WindowBase window))
        {
            window.OnClose(transitionTime);
            if(transitionTime==0) Destroy(window.gameObject);//��Ҫ����ʱ����UI�������о����ر�ʱ��
        }
    }
    /// <summary>
    /// �ر����д���
    /// </summary>
    public void CloseAllWindow()
    {
        foreach (UI_WindowBase window in windowDic.Values)//�������д���
        {
            window.OnClose();
            Destroy(window.gameObject);
        }
        windowDic.Clear();//���
    }
    public void CloseAllWindow(EnumUI_Layer layer)
    {


        foreach (Transform item in CheackLayer(layer))//�������д���
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
    /// ���UI�㼶
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
