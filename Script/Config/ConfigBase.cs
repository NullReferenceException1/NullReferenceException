using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///���������
/// </summary>
public class ConfigBase : ScriptableObject
{
    [BoxGroup("��������"), HideLabel]
    [EnumPaging]
    public EnumConfigType configType;
}
