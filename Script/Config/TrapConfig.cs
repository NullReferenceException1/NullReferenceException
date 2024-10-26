using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///陷阱配置
/// </summary>
[CreateAssetMenu(menuName = "Config/GameSceneConfig/TrapConfig", fileName = "TrapConfig")]
public class TrapConfig : ConfigBase
{
    [PropertySpace]
    [BoxGroup("伤害"),HideLabel]
    public float Value;
    [BoxGroup("攻击的时间间隔")]
    public float intervalTime=1;
    [BoxGroup("击退力量"), HideLabel]
    public float RepellingForce=3.5F;
    [BoxGroup("击退时间"), HideLabel]
    public float RepellingTime=0.2F;
    [BoxGroup("击退效果"), HideLabel]
    public Vector2 RepellingEffect=new Vector2(1,2);
}
