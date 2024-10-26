using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalManager 
{
    void Init();
}
/// <summary>
///全局管理器(管理场景中所有管理器)
/// </summary>
public class GlobalManagerBase<T> : MonoBehaviour, IGlobalManager where T : GlobalManagerBase<T>
{
   public static T Instance;
    public virtual void Init()
    {
        Instance = (T)this;
    }
}
