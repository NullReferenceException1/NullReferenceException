using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalManager 
{
    void Init();
}
/// <summary>
///ȫ�ֹ�����(�����������й�����)
/// </summary>
public class GlobalManagerBase<T> : MonoBehaviour, IGlobalManager where T : GlobalManagerBase<T>
{
   public static T Instance;
    public virtual void Init()
    {
        Instance = (T)this;
    }
}
