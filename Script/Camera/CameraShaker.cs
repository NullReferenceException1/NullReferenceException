
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///�����
/// </summary>

public class CameraShaker : MonoBehaviour
{
    public CinemachineImpulseSource cinemachineImpulse;
    public float shakerAmont;//�𶯴�С 
    public void Init()
    {
        Event_Manager.Instance.AddEventListener(EnumEventType.�����¼�, SceneShaker);
    }
    /// <summary>
    /// ��Ļ��Ч��
    /// </summary>
    [Button]
    public void SceneShaker()
    {
        cinemachineImpulse.GenerateImpulse(shakerAmont);
    }
}
