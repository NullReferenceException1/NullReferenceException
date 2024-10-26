
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///相机震动
/// </summary>

public class CameraShaker : MonoBehaviour
{
    public CinemachineImpulseSource cinemachineImpulse;
    public float shakerAmont;//震动大小 
    public void Init()
    {
        Event_Manager.Instance.AddEventListener(EnumEventType.受伤事件, SceneShaker);
    }
    /// <summary>
    /// 屏幕震动效果
    /// </summary>
    [Button]
    public void SceneShaker()
    {
        cinemachineImpulse.GenerateImpulse(shakerAmont);
    }
}
