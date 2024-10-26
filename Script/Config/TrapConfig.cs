using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///��������
/// </summary>
[CreateAssetMenu(menuName = "Config/GameSceneConfig/TrapConfig", fileName = "TrapConfig")]
public class TrapConfig : ConfigBase
{
    [PropertySpace]
    [BoxGroup("�˺�"),HideLabel]
    public float Value;
    [BoxGroup("������ʱ����")]
    public float intervalTime=1;
    [BoxGroup("��������"), HideLabel]
    public float RepellingForce=3.5F;
    [BoxGroup("����ʱ��"), HideLabel]
    public float RepellingTime=0.2F;
    [BoxGroup("����Ч��"), HideLabel]
    public Vector2 RepellingEffect=new Vector2(1,2);
}
