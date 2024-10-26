using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///配置类基类
/// </summary>
public class ConfigBase : ScriptableObject
{
    [BoxGroup("配置类型"), HideLabel]
    [EnumPaging]
    public EnumConfigType configType;
}
