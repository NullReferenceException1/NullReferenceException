using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///��ì����
/// </summary>
public class TrapLance : TrapBase
{
    /// <summary>
    /// ��������
    /// </summary>
    public void TriggerTrap()
    {
        animator.Play("��ì����");
    }
}
