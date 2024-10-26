
using UnityEngine;

/// <summary>
///资源管理器
/// </summary>
public class Res_Manager : GlobalManagerBase<Res_Manager>
{

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        string headPath =typeof(T).Name;
        return Resources.Load<T>(headPath+"/"+path);
    }
}
